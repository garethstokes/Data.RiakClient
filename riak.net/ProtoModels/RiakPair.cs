using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using System.ComponentModel;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbPair")]
    public class RiakPair : IExtensible
    {
        private byte[] _key;
        [ProtoMember(1, IsRequired = true, Name = @"key", DataFormat = DataFormat.Default)]
        public byte[] Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private byte[] _value = null;
        [ProtoMember(2, IsRequired = false, Name = @"value", DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] Value
        {
            get { return _value; }
            set { _value = value; }
        }
        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
