namespace System.Data.RiakClient.Models
{
    public enum RequestMethod
    {
        Ping                = 1,
        FindClientId        = 3,
        PersistClientId     = 5,
        ServerInfo          = 7,
        Find                = 9,
        Perist              = 11,
        Detach              = 13,
        ListBuckets         = 15,
        ListKeys            = 17
    }
}
