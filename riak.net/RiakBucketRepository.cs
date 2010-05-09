using System.Linq;
using System.Data.RiakClient.Models;
using System.Data.RiakClient.Helpers;

namespace System.Data.RiakClient
{
    public class RiakBucketRepository
    {
        private RiakConnectionManager _connectionManager;

        public RiakBucketRepository(RiakConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public RiakResponse<string[]> ListBuckets()
        {
            var s = _connectionManager.GetNextConnection();
            
            var message = PackagedMessage.JustHeader(RequestMethod.ListBuckets);
            s.Write(message, 0, message.Length);

            var response = PackagedMessage.GetResponse<ListBucketsResponse>(s);
            return response.Buckets.Count() == 0
                ? RiakResponse<string[]>.WithErrors("No buckets")
                : RiakResponse<string[]>.WithoutErrors(response.Buckets.Select(x => x.DecodeToString()).ToArray());
        }
    }
}
