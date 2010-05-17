using System.Linq;
using System.Data.RiakClient.Models;

namespace System.Data.RiakClient
{
    public class RiakDocumentRepository
    {
        private RiakConnectionManager _connectionManager;

        public RiakDocumentRepository(RiakConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public RiakResponse<RiakDocument> Find(FindRequest request)
        {
            var connection = _connectionManager.GetNextConnection();
            var r = connection.WriteWith(request, RequestMethod.Find);
            if (r.ResponseCode == RiakResponseCode.Failed)
            {
                return RiakResponse<RiakDocument>.WithErrors(r.Messages);
            }

            var response = connection.Read<FindResponse>();
            if (response.ResponseCode == RiakResponseCode.Failed)
            {
                return RiakResponse<RiakDocument>.WithErrors(r.Messages);
            }

            return response.Result.Content.Count() == 0 || response.Result.Content.FirstOrDefault() == null
                ? RiakResponse<RiakDocument>.WithErrors("No documents found")
                : RiakResponse<RiakDocument>.WithoutErrors(response.Result.Content.First());
        }

        public RiakResponse<RiakDocument> Find(Action<FindRequest> predicate)
        {
            var request = new FindRequest();
            predicate(request);
            return Find(request);
        }

        public RiakResponse<RiakDocument[]> Find(string[] keys, Action<FindRequest> requestParameters)
        {
            RiakResponse<RiakDocument[]> result = RiakResponse<RiakDocument[]>.WithoutErrors(new RiakDocument[] {});
            foreach(var key in keys)
            {
                var request = new FindRequest() {Key = key.GetBytes()};
                requestParameters.Invoke(request);
                var result = Find(request);
                
            }
        }

        public RiakResponse<RiakDocument> Persist(PersistRequest request)
        {
            var connection = _connectionManager.GetNextConnection();
            var r = connection.WriteWith(request, RequestMethod.Perist);
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
        
        public RiakResponse<RiakDocument> Persist(Action<PersistRequest> predicate)
        {
            var request = new PersistRequest();
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
