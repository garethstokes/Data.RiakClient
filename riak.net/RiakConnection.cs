using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using ProtoBuf;
using System.Data.RiakClient.Helpers;
using System.Net;
using System.Data.RiakClient.Models;

namespace System.Data.RiakClient
{
    public class RiakConnection
    {
        private readonly string _host;
        private readonly int _port;

        public RiakConnection(string host, int port)
        {
            //create socket connection.
            _host = host;
            _port = port;
        }

        public RiakDocument Find(RiakFindRequest request)
        {
            var stream = GetConnectionStream();
            var message = PackageMessageFrom(request, 09);
            stream.Write(message, 0, message.Length);

            var response = GetResponse<RiakGetResponse>(stream);
            return response.Content.FirstOrDefault();
        }

        public string Persist(RiakPersistRequest request)
        {
            var s = GetConnectionStream();
            var message = PackageMessageFrom(request, 11);
            s.Write(message, 0, message.Length);

            var response = GetResponse<RiakPutResponse>(s);
            return request.Key.DecodeToString();
        }

        public bool Detach(RiakDetachRequest request)
        {
            var s = GetConnectionStream();
            var message = PackageMessageFrom(request, 13);
            s.Write(message, 0, message.Length);
            return true;
        }

        public byte[] PackageMessageFrom<T>(T request, int method)
        {
            var documentStream = new MemoryStream();
            Serializer.Serialize(documentStream, request);

            var bytes = documentStream.GetBytes();
            var length = bytes.Length + 1;
            var size = IPAddress.HostToNetworkOrder(length);

            return BitConverter.GetBytes(size)
                               .Concat(new[] { Convert.ToByte(method) })
                               .Concat(bytes)
                               .ToArray();
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
