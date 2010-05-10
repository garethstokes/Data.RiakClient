using System.Collections.Generic;
using System.Linq;
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

        public RiakConnection GetNextConnection()
        {
            return _connections.First();
        }

        public static RiakConnectionManager FromConfiguration 
        { 
            get
            {
                return new RiakConnectionManager();
            }
        }
    }
}
