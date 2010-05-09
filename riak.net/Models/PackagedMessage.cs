using System.IO;
using ProtoBuf;
using System.Data.RiakClient.Helpers;
using System.Net;
using System.Linq;

namespace System.Data.RiakClient.Models
{
    public class PackagedMessage
    {
        public static byte[] From<T>(T request, RequestMethod method)
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

        public static byte[] JustHeader(RequestMethod requestMethod)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(1))
                               .Concat(new[] {Convert.ToByte(requestMethod)})
                               .ToArray();
        }

        internal static T GetResponse<T>(Stream stream) where T : new()
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
    }
}
