using System.Linq;
using System.Data.RiakClient.Models;

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
            var connection = _connectionManager.GetNextConnection();
            var r = connection.WriteWithoutRequestBody(new string[] {}, RequestMethod.ListBuckets);
            if (r.ResponseCode == RiakResponseCode.Failed)
            {
                return RiakResponse<string[]>.WithErrors(r.Messages);
            }

            var response = connection.Read<ListBucketsResponse>();
            return response.ResponseCode == RiakResponseCode.Failed || response.Result.Buckets.Count() == 0
                ? RiakResponse<string[]>.WithErrors("No buckets", response.Messages.First())
                : RiakResponse<string[]>.WithoutErrors(response.Result
                                                               .Buckets
                                                               .Select(x => x.DecodeToString())
                                                               .ToArray());
        }

        public RiakResponse<string[]> ListKeysFor(ListKeysRequest request)
        {
            return null;
        }
    }
}
