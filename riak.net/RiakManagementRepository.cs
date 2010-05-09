using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.RiakClient.Models;
using System.Data.RiakClient.Helpers;

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
            var response = Connection.WriteWithoutRequestBody(false, RequestMethod.Ping);
            if (response.ResponseCode == RiakResponseCode.Successful) response.Result = true;
            return response;
        }

        public RiakResponse<string> FindClientId()
        {
            var r = Connection.WriteWithoutRequestBody(new byte[] {}, RequestMethod.FindClientId);
            return r.ResponseCode == RiakResponseCode.Failed 
                ? RiakResponse<string>.WithErrors(r.Result.DecodeToString(), r.Messages) 
                : RiakResponse<string>.WithoutErrors(r.Result.DecodeToString());
        }
    }
}
