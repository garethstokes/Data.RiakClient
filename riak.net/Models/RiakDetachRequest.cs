using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.RiakClient.Models;
using System.Data.RiakClient;

namespace riak.net.Models
{
    public class RiakDetachRequest
    {
        public RiakDetachRequest ()
        {
            SuccessfulDeleteCount = 3;
        }

        public uint SuccessfulDeleteCount { get; set; }
        public string Bucket { get; set; }
        public String Key { get; set; }

        public DetachRequest ProxyRequest()
        {
            return new DetachRequest() {
                Bucket = Bucket.GetBytes(),
                Key = Key.GetBytes(), 
                SuccessfulDeleteCount = SuccessfulDeleteCount
            };
        }
    }
}
