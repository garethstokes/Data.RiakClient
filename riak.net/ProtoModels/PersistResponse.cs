using System.Collections.Generic;
using ProtoBuf;
using riak.net.ProtoModels;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbPutResp")]
    public class PersistResponse : IExtensible
    {
        private readonly List<RiakContent> _content = new List<RiakContent>();
        [ProtoMember(1, Name = @"content", DataFormat = DataFormat.Default)]
        public List<RiakContent> Content
        {
            get { return _content; }
        }

        private byte[] _vclock = null;
        [ProtoMember(2, IsRequired = false, Name = @"vclock", DataFormat = DataFormat.Default)]
        [ComponentModel.DefaultValue(null)]
        public byte[] VectorClock
        {
            get { return _vclock; }
            set { _vclock = value; }
        }

        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
