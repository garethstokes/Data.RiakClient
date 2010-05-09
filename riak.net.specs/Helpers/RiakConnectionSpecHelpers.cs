using System.Text;
using System.Data.RiakClient.Models;
using System.Data.RiakClient.Helpers;

namespace riak.net.specs.Helpers
{
    public static class RiakConnectionSpecHelpers
    {
        public static RiakPersistRequest GetPersistRequest(this RiakConnectionSpecs specs)
        {
            return new RiakPersistRequest {
                Bucket = Encoding.UTF8.GetBytes("test_bucket"),
                Key = Encoding.UTF8.GetBytes("test_key"),
                Content = new RiakDocument {
                    Value = Encoding.UTF8.GetBytes("this is a test")
                }
            };
        }

        public static RiakFindRequest GetFindRequest(this RiakConnectionSpecs specs)
        {
            return new RiakFindRequest {
                Bucket = "test".GetBytes(),
                Key = "123".GetBytes()
            };
        }

        public static RiakDetachRequest GetDetachRequest(this RiakConnectionSpecs specs)
        {
            return new RiakDetachRequest {
                Bucket = "test".GetBytes(),
                Key = "123".GetBytes()
            };
        }

        public static RiakDocument GetTestDocument(this RiakConnectionSpecs specs)
        {
            return new RiakDocument {
                    Value = Encoding.UTF8.GetBytes("this is a test"),
                    ContentType = Encoding.UTF8.GetBytes("text/plain")
                };
        }
    }
}
