using System.Collections.Generic;
using ProtoBuf;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbPutResp")]
    public class PersistResponse : IExtensible
    {
        private readonly List<RiakDocument> _contents = new List<RiakDocument>();
        [ProtoMember(1, Name = @"contents", DataFormat = DataFormat.Default)]
        public List<RiakDocument> Contents
        {
            get { return _contents; }
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
