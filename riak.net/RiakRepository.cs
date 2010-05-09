using System.Linq;
using System.Net.Sockets;
using System.IO;
using ProtoBuf;
using System.Net;
using System.Data.RiakClient.Models;

namespace System.Data.RiakClient
{
    public class RiakDocumentRepository
    {
        private readonly string _host;
        private readonly int _port;

        public RiakDocumentRepository(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public RiakResponse<RiakDocument> Find(FindRequest request)
        {
            var stream = GetConnectionStream();
            var message = PackagedMessage.From(request, RequestMethod.Find);
            stream.Write(message, 0, message.Length);

            var response = GetResponse<FindResponse>(stream);
            return response.Content.Count() == 0 || response.Content.FirstOrDefault() == null
                ? RiakResponse<RiakDocument>.WithErrors("No documents found")
                : RiakResponse<RiakDocument>.WithoutErrors(response.Content.First());
        }

        public RiakResponse<RiakDocument> Persist(PersistRequest request)
        {
            var s = GetConnectionStream();
            var message = PackagedMessage.From(request, RequestMethod.Perist);
            s.Write(message, 0, message.Length);

            var response = GetResponse<PersistResponse>(s);
            return response.VectorClock == null
                ? RiakResponse<RiakDocument>.WithErrors("unknown error on persist")
                : RiakResponse<RiakDocument>.WithoutErrors(response.Contents.FirstOrDefault());
        }

        public RiakResponse<bool> Detach(DetachRequest request)
        {
            var s = GetConnectionStream();
            var message = PackagedMessage.From(request, RequestMethod.Detach);
            s.Write(message, 0, message.Length);
            return RiakResponse<bool>.WithoutErrors(true);
        }

        private T GetResponse<T>(Stream stream) where T : new()
        {
            // Get the length of the stream.
            var length = new byte[4];
            stream.Read(length, 0, 4);
            var size = BitConverter.ToInt32(length, 0);
            size = IPAddress.NetworkToHostOrder(size);
            
            if (size == 1) return new T();

            // use this to figure out what type to deserialize 
            var method = stream.ReadByte();

            var receipt = new byte[size - 1];
            stream.Read(receipt, 0, size - 1);

            return Serializer.Deserialize<T>(new MemoryStream(receipt));
        }

        private Stream GetConnectionStream()
        {
            var client = new TcpClient(_host, _port);
            return client.GetStream();
        }
    }
}
