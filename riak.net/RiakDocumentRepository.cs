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
            var stream = _connectionManager.GetNextConnectionStream();
            var request = new FindRequest();
            predicate(request);

            var message = PackagedMessage.From(request, RequestMethod.Find);
            stream.Write(message, 0, message.Length);

            var response = PackagedMessage.GetResponse<FindResponse>(stream);
            return response.Content.Count() == 0 || response.Content.FirstOrDefault() == null
                ? RiakResponse<RiakDocument>.WithErrors("No documents found")
                : RiakResponse<RiakDocument>.WithoutErrors(response.Content.First());
        }

        public RiakResponse<RiakDocument> Persist(Action<PersistRequest> predicate)
        {
            var s = _connectionManager.GetNextConnectionStream();
            var request = new PersistRequest();
            predicate(request);

            var message = PackagedMessage.From(request, RequestMethod.Perist);
            s.Write(message, 0, message.Length);

            var response = PackagedMessage.GetResponse<PersistResponse>(s);
            return response.VectorClock == null
                ? RiakResponse<RiakDocument>.WithErrors("unknown error on persist")
                : RiakResponse<RiakDocument>.WithoutErrors(response.Contents.FirstOrDefault());
        }

        public RiakResponse<bool> Detach(Action<DetachRequest> predicate)
        {
            var s = _connectionManager.GetNextConnectionStream();
            var request = new DetachRequest();
            predicate(request);

            var message = PackagedMessage.From(request, RequestMethod.Detach);
            s.Write(message, 0, message.Length);
            return RiakResponse<bool>.WithoutErrors(true);
        }
    }
}
