﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using System.ComponentModel;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbDelReq")]
    public class DetachRequest : IExtensible
    {
        private byte[] _bucket;
        private byte[] _key;
        private uint _rw = default(uint);

        [ProtoMember(1, IsRequired = true, Name = @"bucket", DataFormat = DataFormat.Default)]
        public byte[] Bucket
        {
            get { return _bucket; }
            set { _bucket = value; }
        }

        [ProtoMember(2, IsRequired = true, Name = @"key", DataFormat = DataFormat.Default)]
        public byte[] Key
        {
            get { return _key; }
            set { _key = value; }
        }

        [ProtoMember(3, IsRequired = false, Name = @"rw", DataFormat = DataFormat.TwosComplement)]
        [DefaultValue(default(uint))]
        public uint SuccessfulDeleteCount
        {
            get { return _rw; }
            set { _rw = value; }
        }

        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
