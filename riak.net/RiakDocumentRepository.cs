using System.Linq;
using System.Data.RiakClient.Models;
using Amib.Threading;

namespace System.Data.RiakClient
{
    public class RiakDocumentRepository
    {
        private RiakConnectionManager _connectionManager;
        private readonly SmartThreadPool _threadPool = new SmartThreadPool();

        public RiakDocumentRepository(RiakConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        
        public RiakResponse<RiakDocument[]> Find(RiakFindRequest request)
        {
            var connection = _connectionManager.GetNextConnection();
            var threads = request.GetInternalRequests()
                                 .Select(r => _threadPool.QueueWorkItem(() => {
                                     connection.WriteWith(r, RequestMethod.Find);
                                     return connection.Read<FindResponse>();
                                 }))
                                 .ToArray();
            SmartThreadPool.WaitAll(threads);

            var responses = threads.Select(t => t.Result).ToArray();
            var badResponses = responses.Where(r => r.ResponseCode == RiakResponseCode.Failed);
            return badResponses.Count() == 0
                ? RiakResponse<RiakDocument[]>.WithoutErrors(responses.Select(x => x.Result.Content.First()).ToArray())
                : RiakResponse<RiakDocument[]>.WithWarning(badResponses.Select(x => x.Messages.LastOrDefault()).ToArray(),
                                                           responses.Select(x => x.Result.Content.FirstOrDefault()).ToArray());
        }

        public RiakResponse<RiakDocument[]> Find(Action<RiakFindRequest> predicate)
        {
            var request = new RiakFindRequest();
            predicate(request);
            return Find(request);
        }

        public RiakResponse<RiakDocument> Persist(RiakPersistRequest request)
        {
            var connection = _connectionManager.GetNextConnection();
            var r = connection.WriteWith(request.ProxyRequest(), RequestMethod.Perist);
            if (r.ResponseCode == RiakResponseCode.Failed)
            {
                return RiakResponse<RiakDocument>.WithErrors(r.Messages);
            }

            var response = connection.Read<PersistResponse>();
            if (response.ResponseCode == RiakResponseCode.Failed)
            {
                return RiakResponse<RiakDocument>.WithErrors(r.Messages);
            }

            return response.Result == null || response.Result.VectorClock == null
                ? RiakResponse<RiakDocument>.WithErrors("Connection was successful but persist failed")
                : RiakResponse<RiakDocument>.WithoutErrors(response.Result.Contents.FirstOrDefault());
        }
        
        public RiakResponse<RiakDocument> Persist(Action<RiakPersistRequest> predicate)
        {
            var request = new RiakPersistRequest();
            predicate(request);
            return Persist(request);
        }

        public RiakResponse<bool> Detach(DetachRequest request)
        {
            var connection = _connectionManager.GetNextConnection();
            var r = connection.WriteWith(request, RequestMethod.Detach);
            return r.ResponseCode == RiakResponseCode.Failed
                ? RiakResponse<bool>.WithErrors(false, r.Messages)
                : RiakResponse<bool>.WithoutErrors(true);
        }

        public RiakResponse<bool> Detach(Action<DetachRequest> predicate)
        {
            var request = new DetachRequest();
            predicate(request);
            return Detach(request);
        }
    }
}
