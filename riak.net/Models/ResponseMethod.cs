namespace System.Data.RiakClient.Models
{
    public enum ResponseMethod
    {
        Error           = 0,
        Ping            = 2,
        FindClientId    = 4,
        PersistClientId = 6,
        ServerInfo      = 8,
        Find            = 10,
        Persist         = 12,
        Detach          = 14,
        ListBuckets     = 16,
        ListKeys        = 18
    }
}
