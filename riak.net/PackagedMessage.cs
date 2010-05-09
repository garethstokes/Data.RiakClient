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
    }
}
