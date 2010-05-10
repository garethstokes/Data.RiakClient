using ProtoBuf;
using System.ComponentModel;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbGetReq")]
    public class FindRequest : IExtensible
    {
        private byte[] _bucket;
        [ProtoMember(1, IsRequired = true, Name = @"bucket", DataFormat = DataFormat.Default)]
        public byte[] Bucket
        {
            get { return _bucket; }
            set { _bucket = value; }
        }
        private byte[] _key;
        [ProtoMember(2, IsRequired = true, Name = @"key", DataFormat = DataFormat.Default)]
        public byte[] Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private uint _r = default(uint);
        [ProtoMember(3, IsRequired = false, Name = @"r", DataFormat = DataFormat.TwosComplement)]
        [DefaultValue(default(uint))]
        public uint ReadValue
        {
            get { return _r; }
            set { _r = value; }
        }

        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
