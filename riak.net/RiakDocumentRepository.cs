using System.Linq;
using System.IO;
using ProtoBuf;
using System.Net;
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

        public RiakResponse<RiakDocument> Find(Action<FindRequest> predicate)
        {
            var connection = _connectionManager.GetNextConnection();
            var request = new FindRequest();
            predicate(request);

            var r = connection.Write(request, RequestMethod.Find);
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

        public RiakResponse<RiakDocument> Persist(Action<PersistRequest> predicate)
        {
            var connection = _connectionManager.GetNextConnection();
            var request = new PersistRequest();
            predicate(request);

            var r = connection.Write(request, RequestMethod.Perist);
            if (r.ResponseCode == RiakResponseCode.Failed)
            {
                return RiakResponse<RiakDocument>.WithErrors(r.Messages);
            }

            var response = connection.Read<PersistResponse>();
            if (response.ResponseCode == RiakResponseCode.Failed)
            {
                return RiakResponse<RiakDocument>.WithErrors(r.Messages);
            }

            return response.VectorClock == null
                ? RiakResponse<RiakDocument>.WithErrors("Connection was successful but persist failed")
                : RiakResponse<RiakDocument>.WithoutErrors(response.Result.Contents.FirstOrDefault());
        }

        public RiakResponse<bool> Detach(Action<DetachRequest> predicate)
        {
            var connection = _connectionManager.GetNextConnection();
            var request = new DetachRequest();
            predicate(request);

            var r = connection.Write(request, RequestMethod.Detach);
            return r.ResponseCode == RiakResponseCode.Failed 
                ? RiakResponse<bool>.WithErrors(false, r.Messages) 
                : RiakResponse<bool>.WithoutErrors(true);
        }
    }
}
