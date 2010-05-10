using System.Collections.Generic;
using ProtoBuf;
using System.ComponentModel;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbContent")]
    public class RiakDocument : IExtensible
    {
        private byte[] _value;
        [ProtoMember(1, IsRequired = true, Name = @"value", DataFormat = DataFormat.Default)]
        public byte[] Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private byte[] _content_type = null;
        [ProtoMember(2, IsRequired = false, Name = @"content_type", DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] ContentType
        {
            get { return _content_type; }
            set { _content_type = value; }
        }

        private byte[] _charset = null;
        [ProtoMember(3, IsRequired = false, Name = @"charset", DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] Charset
        {
            get { return _charset; }
            set { _charset = value; }
        }

        private byte[] _content_encoding = null;
        [ProtoMember(4, IsRequired = false, Name = @"content_encoding", DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] ContentEncoding
        {
            get { return _content_encoding; }
            set { _content_encoding = value; }
        }

        private byte[] _vtag = null;
        [ProtoMember(5, IsRequired = false, Name = @"vtag", DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] vtag
        {
            get { return _vtag; }
            set { _vtag = value; }
        }
        private readonly List<RpbLink> _links = new List<RpbLink>();
        [ProtoMember(6, Name = @"links", DataFormat = DataFormat.Default)]
        public List<RpbLink> Links
        {
            get { return _links; }
        }


        private uint _last_mod = default(uint);
        [ProtoMember(7, IsRequired = false, Name = @"last_mod", DataFormat = DataFormat.TwosComplement)]
        [DefaultValue(default(uint))]
        public uint LastMod
        {
            get { return _last_mod; }
            set { _last_mod = value; }
        }

        private uint _last_mod_usecs = default(uint);
        [ProtoMember(8, IsRequired = false, Name = @"last_mod_usecs", DataFormat = DataFormat.TwosComplement)]
        [DefaultValue(default(uint))]
        public uint LastModSecs
        {
            get { return _last_mod_usecs; }
            set { _last_mod_usecs = value; }
        }

        private readonly List<RiakPair> _usermeta = new List<RiakPair>();
        [ProtoMember(9, Name = @"usermeta", DataFormat = DataFormat.Default)]
        public List<RiakPair> UserMetadata
        {
            get { return _usermeta; }
        }

        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
