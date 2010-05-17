using ProtoBuf;
using System.ComponentModel;

namespace System.Data.RiakClient.Models
{
    [Serializable, ProtoContract(Name = @"RpbPutReq")]
    public class PersistRequest : IExtensible
    {
        private byte[] _key;
        private byte[] _bucket;
        private byte[] _vclock = null;
        private RiakDocument _content;    
        private uint _w = default(uint);
        private bool _return_body = default(bool);
        
        [ProtoMember(1, 
                     IsRequired = true, 
                     Name = @"bucket", 
                     DataFormat = DataFormat.Default)]
        public byte[] Bucket
        {
            get { return _bucket; }
            set { _bucket = value; }
        }

        [ProtoMember(2, 
                     IsRequired = true, 
                     Name = @"key", 
                     DataFormat = DataFormat.Default)]
        public byte[] Key
        {
            get { return _key; }
            set { _key = value; }
        }

        [ProtoMember(3, 
                     IsRequired = false, 
                     Name = @"vclock", 
                     DataFormat =  DataFormat.Default)]
        [DefaultValue(null)]
        public byte[] VClock
        {
            get { return _vclock; }
            set { _vclock = value; }
        }


        [ProtoMember(4,
                     IsRequired = true,
                     Name = @"content",
                     DataFormat =  DataFormat.Default)]
        public RiakDocument Content
        {
            get { return _content; }
            set { _content = value; }
        }


        [ProtoMember(5, 
                     IsRequired = false, 
                     Name = @"w", 
                     DataFormat =  DataFormat.TwosComplement)]
        [DefaultValue(default(uint))]
        public uint Write
        {
            get { return _w; }
            set { _w = value; }
        }

        private uint _dw = default(uint);
        [ProtoMember(6, 
                     IsRequired = false, 
                     Name = @"dw", 
                     DataFormat =  DataFormat.TwosComplement)]
        [DefaultValue(default(uint))]
        public uint DW
        {
            get { return _dw; }
            set { _dw = value; }
        }


        [ProtoMember(7, 
                     IsRequired = false, 
                     Name = @"return_body", 
                     DataFormat =  DataFormat.Default)]
        [DefaultValue(default(bool))]
        public bool ReturnBody
        {
            get { return _return_body; }
            set { _return_body = value; }
        }

        private  IExtension extensionObject;
        IExtension  IExtensible.GetExtensionObject(bool createIfMissing)
        { return  Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
