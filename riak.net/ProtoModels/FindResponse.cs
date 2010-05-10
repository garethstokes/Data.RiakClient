using System.Collections.Generic;
using ProtoBuf;
using System.ComponentModel;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbGetResp")]
    public class FindResponse : IExtensible
    {
        private readonly List<RiakDocument> _content = new List<RiakDocument>();
        private byte[] _vectorClock = null;
        
        
        [ProtoMember(1, Name = @"content", DataFormat = DataFormat.Default)]
        public List<RiakDocument> Content
        {
            get { return _content; }
        }

        [ProtoMember(2, IsRequired = false, Name = @"vclock", DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] VectorClock
        {
            get { return _vectorClock; }
            set { _vectorClock = value; }
        }

        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
