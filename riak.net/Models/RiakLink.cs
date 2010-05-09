using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using System.ComponentModel;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbLink")]
    public class RpbLink : IExtensible
    {
        private byte[] _bucket = null;
        [ProtoMember(1, IsRequired = false, Name = @"bucket", DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] Bucket
        {
            get { return _bucket; }
            set { _bucket = value; }
        }

        private byte[] _key = null;
        [ProtoMember(2, IsRequired = false, Name = @"key", DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private byte[] _tag = null;
        [ProtoMember(3, IsRequired = false, Name = @"tag", DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
