using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.RiakClient.Models;
using System.Net.Sockets;

namespace System.Data.RiakClient
{
    public class RiakManagementRepository
    {
        public RiakConnection Connection { get; set; }

        public RiakManagementRepository(RiakConnection connection)
        {
            Connection = connection;
        }

        public RiakResponse<bool> Ping()
        {
            var s = Connection.Stream;

            try
            {
                var message = PackagedMessage.JustHeader(RequestMethod.Ping);
                s.Write(message, 0, message.Length);
            }
            catch (SocketException e)
            {
                return RiakResponse<bool>.WithErrors(false, "Could not establish connection", e.Message);
            }

            return RiakResponse<bool>.WithoutErrors(true);
        }
    }
}
