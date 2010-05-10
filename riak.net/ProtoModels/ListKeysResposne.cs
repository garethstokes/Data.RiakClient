using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Data.RiakClient.Models
{
    [Serializable]
    [ProtoContract(Name = @"RpbListKeysResp")]
    public class RpbListKeysResp : IExtensible
    {
        private readonly List<byte[]> _keys = new List<byte[]>();
        private bool _done = default(bool);
        
        [ProtoMember(1, 
                     Name = @"keys", 
                     DataFormat = DataFormat.Default)]
        public List<byte[]> Keys
        {
            get { return _keys; }
        }

        [ProtoMember(2, 
                     IsRequired = false, 
                     Name = @"done", 
                     DataFormat = DataFormat.Default)]
        [DefaultValue(default(bool))]
        public bool Done
        {
            get { return _done; }
            set { _done = value; }
        }

        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
