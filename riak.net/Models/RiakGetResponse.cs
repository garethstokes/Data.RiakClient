using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using System.ComponentModel;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbGetResp")]
    public class RiakGetResponse : IExtensible
    {
        private readonly List<RiakDocument> _content = new List<RiakDocument>();
        [ProtoMember(1, Name = @"content", DataFormat = DataFormat.Default)]
        public List<RiakDocument> Content
        {
            get { return _content; }
        }


        private byte[] _vclock = null;
        [ProtoMember(2, IsRequired = false, Name = @"vclock", DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
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
