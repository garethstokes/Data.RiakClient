namespace System.Data.RiakClient.Models
{
    public enum ResponseMethod
    {
        Error       = 0,
        Ping        = 2,
        GetClientId = 4,
        SetClientId = 6,
        ServerInfo  = 8,
        Find        = 10,
        Persist     = 12,
        Detach      = 14,
        ListBuckets = 16,
        ListKeys    = 18
    }
}
