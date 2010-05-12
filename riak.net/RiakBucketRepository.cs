using System.Linq;
using System.Data.RiakClient.Models;
using System.Collections.Generic;

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
            var r = connection.WriteRequestWithoutBody(new string[] {}, RequestMethod.ListBuckets);
            if (r.ResponseCode == RiakResponseCode.Failed)
            {
                return RiakResponse<string[]>.WithErrors(r.Messages);
            }

            var response = connection.Read<ListBucketsResponse>();
            return response.ResponseCode == RiakResponseCode.Failed || response.Result.Buckets.Count() == 0
                ? RiakResponse<string[]>.WithErrors("No buckets", response.Messages.FirstOrDefault())
                : RiakResponse<string[]>.WithoutErrors(response.Result
                                                               .Buckets
                                                               .Select(x => x.DecodeToString())
                                                               .ToArray());
        }

        public RiakResponse<string[]> ListKeysFor(ListKeysRequest request) {
            var connection = _connectionManager.GetNextConnection();
            var writeResponse = connection.WriteWith(requestObject: request, 
                                                     method: RequestMethod.ListKeys);
            
            // Keep looping untill all the results are returned.
            return writeResponse.ResponseCode == RiakResponseCode.Failed
                ? RiakResponse<string[]>.WithErrors(writeResponse.Messages)
                : RiakResponse<string[]>.ReadResponse(() => {
                    var dontFinish = true;
                    var keys = new List<string>();
                    while(dontFinish)
                    {
                        var readResponse = connection.Read<ListKeysResponse>();
                        if (readResponse.ResponseCode == RiakResponseCode.Failed)
                        {
                            return RiakResponse<string[]>.WithErrors(keys.ToArray(), readResponse.Messages);
                        }
                        keys.AddRange(readResponse.Result.Keys.Select(x => x.DecodeToString()).ToArray());
                        dontFinish = readResponse.Result.Done == false;
                    }

                    return RiakResponse<string[]>.WithoutErrors(keys.ToArray());
            });
        }
    }
}
