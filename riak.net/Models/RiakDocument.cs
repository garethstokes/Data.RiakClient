using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using riak.net.ProtoModels;
using System.Data.RiakClient;

namespace riak.net.Models
{
    public class RiakDocument
    {
        public string Charset { get; set; }
        public string ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public uint LastModified { get; set; }
        public uint LastModifiedInSeconds { get; set; }
        public string Value { get; set; }
        public string VectorClock { get; set; }

        public RiakContent ProxyContent()
        {
            return new RiakContent()
                       {
                Charset = Charset.GetBytes(), 
                ContentEncoding = ContentEncoding.GetBytes(), 
                ContentType = ContentType.GetBytes(), 
                LastMod = LastModified, 
                LastModSecs = LastModifiedInSeconds, 
                Value = Value.GetBytes(), 
                vtag = VectorClock.GetBytes()
            };
        }
    }
}
