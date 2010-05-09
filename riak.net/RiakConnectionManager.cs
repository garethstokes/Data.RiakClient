using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Data.RiakClient.Models;

namespace System.Data.RiakClient 
{
    public class RiakConnectionManager
    {
        private readonly List<RiakConnection> _connections = new List<RiakConnection>();

        public RiakConnection AddConnection(string host, int port)
        {
            var c = new RiakConnection {
                Host = host, 
                Port = port
            };
            _connections.Add(c);
            return c;
        }

        public Stream GetNextConnection()
        {
            var connection = _connections.First();
            var client = new TcpClient(connection.Host, connection.Port);
            return client.GetStream();
        }

        public static RiakConnectionManager FromConfiguration { get; }
    }
}
