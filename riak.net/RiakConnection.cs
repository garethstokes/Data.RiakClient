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
    }
}
