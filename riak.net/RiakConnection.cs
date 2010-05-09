using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace System.Data.RiakClient.Models
{
    public class RiakConnection
    {
        public int Port { get; set; }
        public string Host { get; set; }

        public Stream Stream
        {
            get
            {
                var client = new TcpClient(Host, Port);
                return client.GetStream();
            }
        }

        public RiakResponse<R> Write<T,R>(T request, RequestMethod method) where R : new()
        {
            try
            {
                var message = PackagedMessage.From(request, RequestMethod.Ping);
                Stream.Write(message, 0, message.Length);
            }
            catch (SocketException e)
            {
                return RiakResponse<R>.WithErrors(new R(), "Could not establish connection", e.Message);
            }

            return RiakResponse<R>.WithoutErrors(new R());
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
