//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: RiakProtocolBuffers.proto
using System.Data.RiakClient.Models;
namespace System.Data.RiakClient
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RpbErrorResp")]
  public partial class RpbErrorResp : global::ProtoBuf.IExtensible
  {
    public RpbErrorResp() {}
    
    private byte[] _errmsg;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"errmsg", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] errmsg
    {
      get { return _errmsg; }
      set { _errmsg = value; }
    }
    private uint _errcode;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"errcode", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint errcode
    {
      get { return _errcode; }
      set { _errcode = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RpbListBucketsResp")]
  public partial class RpbListBucketsResp : global::ProtoBuf.IExtensible
  {
    public RpbListBucketsResp() {}
    
    private readonly global::System.Collections.Generic.List<byte[]> _buckets = new global::System.Collections.Generic.List<byte[]>();
    [global::ProtoBuf.ProtoMember(1, Name=@"buckets", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<byte[]> buckets
    {
      get { return _buckets; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RpbListKeysReq")]
  public partial class RpbListKeysReq : global::ProtoBuf.IExtensible
  {
    public RpbListKeysReq() {}
    
    private byte[] _bucket;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"bucket", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] bucket
    {
      get { return _bucket; }
      set { _bucket = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RpbListKeysResp")]
  public partial class RpbListKeysResp : global::ProtoBuf.IExtensible
  {
    public RpbListKeysResp() {}
    
    private readonly global::System.Collections.Generic.List<byte[]> _keys = new global::System.Collections.Generic.List<byte[]>();
    [global::ProtoBuf.ProtoMember(1, Name=@"keys", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<byte[]> keys
    {
      get { return _keys; }
    }
  

    private bool _done = default(bool);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"done", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool done
    {
      get { return _done; }
      set { _done = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RpbGetClientIdResp")]
  public partial class RpbGetClientIdResp : global::ProtoBuf.IExtensible
  {
    public RpbGetClientIdResp() {}
    
    private byte[] _client_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"client_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] client_id
    {
      get { return _client_id; }
      set { _client_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RpbSetClientIdReq")]
  public partial class RpbSetClientIdReq : global::ProtoBuf.IExtensible
  {
    public RpbSetClientIdReq() {}
    
    private byte[] _client_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"client_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] client_id
    {
      get { return _client_id; }
      set { _client_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RpbGetServerInfoResp")]
  public partial class RpbGetServerInfoResp : global::ProtoBuf.IExtensible
  {
    public RpbGetServerInfoResp() {}
    

    private byte[] _node = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"node", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] node
    {
      get { return _node; }
      set { _node = value; }
    }

    private byte[] _server_version = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"server_version", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] server_version
    {
      get { return _server_version; }
      set { _server_version = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}