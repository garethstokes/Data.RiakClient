using ProtoBuf;
using System.Collections.Generic;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbListBucketsResp")]
    public class ListBucketsResponse : IExtensible
    {
        private readonly List<byte[]> _buckets = new List<byte[]>();
        [ProtoMember(1, Name = @"buckets", DataFormat = DataFormat.Default)]
        public List<byte[]> Buckets
        {
            get { return _buckets; }
        }

        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
