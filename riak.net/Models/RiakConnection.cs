using System.IO;
using System.Net.Sockets;

namespace System.Data.RiakClient.Models
{
    public class RiakConnection
    {
        public int Port { get; set; }
        public string Host { get; set; }
        private Stream _stream;

        public Stream Stream
        {
            get { return _stream ?? (_stream = new TcpClient(Host, Port).GetStream()); }
        }

        public RiakResponse<T> Read<T>() where T : new()
        {
            try
            {
                var r = PackagedMessage.GetResponse<T>(_stream);
                return RiakResponse<T>.WithoutErrors(r);
            }
            catch (SocketException e)
            {
                return RiakResponse<T>.WithErrors("Could not establish connection", e.Message);
            }
        }

        public RiakResponse<T> WriteWith<T>(T requestObject, RequestMethod method)
        {
            try
            {
                var message = PackagedMessage.From(requestObject, method);
                Stream.Write(message, 0, message.Length);
            }
            catch (SocketException e)
            {
                return RiakResponse<T>.WithErrors(requestObject, "Could not establish connection", e.Message);
            }

            return RiakResponse<T>.WithoutErrors(requestObject);
        }

        public RiakResponse<TR> WriteRequestWithoutBody<TR>(TR defaultValue, RequestMethod method)
        {
            try
            {
                var message = PackagedMessage.JustHeader(method);
                Stream.Write(message, 0, message.Length);
            }
            catch (SocketException e)
            {
                return RiakResponse<TR>.WithErrors(defaultValue, "Could not establish connection", e.Message);
            }

            return RiakResponse<TR>.WithoutErrors(defaultValue);
        }
    }
}
