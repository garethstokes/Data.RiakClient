using ProtoBuf;
using System.ComponentModel;

namespace System.Data.RiakClient.Models
{
    [Serializable]
    [ProtoContract(Name = @"RpbGetServerInfoResp")]
    public class GetServerInformationResponse : IExtensible
    {
        private byte[] _node = null;
        private byte[] _serverVersion = null;
        
        [ProtoMember(1, 
                     IsRequired = false, 
                     Name = @"node", 
                     DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] Node
        {
            get { return _node; }
            set { _node = value; }
        }

        [ProtoMember(2, 
                     IsRequired = false, 
                     Name = @"server_version", 
                     DataFormat = DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] ServerVersion
        {
            get { return _serverVersion; }
            set { _serverVersion = value; }
        }
        private byte[] _server_version = null;
        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
