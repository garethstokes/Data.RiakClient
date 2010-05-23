using System.Linq;
using System.Data.RiakClient.Models;

namespace System.Data.RiakClient
{
    public class RiakFindRequest
    {
        public RiakFindRequest() { ReadValue = 2; }

        public string[] Keys { get; set; }
        public string Bucket { get; set; }
        public int ReadValue { get; set; } 

        internal FindRequest[] GetInternalRequests()
        {
            return Keys.Select(key => new FindRequest {
                Bucket = Bucket.GetBytes(),
                Key = key.GetBytes(),
                ReadValue = Convert.ToUInt32(ReadValue)
            }).ToArray();
        }
    }
}
