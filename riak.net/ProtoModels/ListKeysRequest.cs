using ProtoBuf;
namespace System.Data.RiakClient.Models
{
    [Serializable]
    [ProtoContract(Name = @"RpbListKeysReq")]
    public class ListKeysRequest : IExtensible
    {
        private byte[] _bucket;
        
        [ProtoMember(1, 
                     IsRequired = true, 
                     Name = @"bucket", 
                     DataFormat = DataFormat.Default)]
        public byte[] Bucket
        {
            get { return _bucket; }
            set { _bucket = value; }
        }


        private IExtension extensionObject;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        { return Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
