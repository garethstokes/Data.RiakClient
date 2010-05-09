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

        public RiakResponse<T> Write<T>(T request, RequestMethod method)
        {
            try
            {
                var message = PackagedMessage.From(request, RequestMethod.Ping);
                Stream.Write(message, 0, message.Length);
            }
            catch (SocketException e)
            {
                return RiakResponse<T>.WithErrors(request, "Could not establish connection", e.Message);
            }

            return RiakResponse<T>.WithoutErrors(request);
        }

        public RiakResponse<TR> WriteWithoutRequestBody<TR>(TR defaultValue, RequestMethod method)
        {
            try
            {
                var message = PackagedMessage.JustHeader(RequestMethod.Ping);
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
