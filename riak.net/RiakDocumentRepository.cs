using System.Linq;
using System.Data.RiakClient.Models;
using Amib.Threading;
using riak.net.Models;
using riak.net.ProtoModels;

namespace System.Data.RiakClient
{
    public class RiakContentRepository
    {
        private RiakConnectionManager _connectionManager;
        private readonly SmartThreadPool _threadPool = new SmartThreadPool();

        public RiakContentRepository(RiakConnectionManager connectionManager)
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
                ? RiakResponse<RiakDocument[]>.WithoutErrors(responses.Select(x => x.Result.Content.First().ToDocument()).ToArray())
                : RiakResponse<RiakDocument[]>.WithWarning(badResponses.Select(x => x.Messages.LastOrDefault()).ToArray(),
                                                           responses.Select(x => {
                                                               var content = x.Result.Content.FirstOrDefault();
                                                               return content == null 
                                                                   ? null 
                                                                   : x.Result.Content.FirstOrDefault().ToDocument();
                                                           }).ToArray());
        }

        public RiakResponse<RiakDocument[]> Find(Action<RiakFindRequest> predicate)
        {
            var request = new RiakFindRequest();
            predicate(request);
            return Find(request);
        }

        public RiakResponse<RiakContent> Persist(RiakPersistRequest request)
        {
            var connection = _connectionManager.GetNextConnection();
            var r = connection.WriteWith(request.ProxyRequest(), RequestMethod.Perist);
            if (r.ResponseCode == RiakResponseCode.Failed)
            {
                return RiakResponse<RiakContent>.WithErrors(r.Messages);
            }

            var response = connection.Read<PersistResponse>();
            if (response.ResponseCode == RiakResponseCode.Failed)
            {
                return RiakResponse<RiakContent>.WithErrors(r.Messages);
            }

            return response.Result == null || response.Result.VectorClock == null
                ? RiakResponse<RiakContent>.WithErrors("Connection was successful but persist failed")
                : RiakResponse<RiakContent>.WithoutErrors(response.Result.Content.FirstOrDefault());
        }
        
        public RiakResponse<RiakContent> Persist(Action<RiakPersistRequest> predicate)
        {
            var request = new RiakPersistRequest();
            predicate(request);
            return Persist(request);
        }

        public RiakResponse<bool> Detach(RiakDetachRequest request)
        {
            var connection = _connectionManager.GetNextConnection();
            var r = connection.WriteWith(request.ProxyRequest(), RequestMethod.Detach);
            return r.ResponseCode == RiakResponseCode.Failed
                ? RiakResponse<bool>.WithErrors(false, r.Messages)
                : RiakResponse<bool>.WithoutErrors(true);
        }

        public RiakResponse<bool> Detach(Action<RiakDetachRequest> predicate)
        {
            var request = new RiakDetachRequest();
            predicate(request);
            return Detach(request);
        }
    }
}
