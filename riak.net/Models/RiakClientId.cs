using ProtoBuf;

namespace System.Data.RiakClient.Models
{
    [Serializable]
    [ProtoContract(Name = @"RpbGetClientIdResp")]
    public class FindClientIdResponse : IExtensible
    {
        private byte[] _clientId;
        
        [ProtoMember(1, 
                     IsRequired = true, 
                     Name = @"client_id", 
                     DataFormat = DataFormat.Default)]
        public byte[] ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }
     
        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
