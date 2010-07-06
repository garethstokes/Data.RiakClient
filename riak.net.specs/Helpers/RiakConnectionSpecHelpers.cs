using System.Text;
using System.Data.RiakClient;
using System.Data.RiakClient.Models;

namespace riak.net.specs.Helpers
{
    public static class SpecHelpers
    {
        public static PersistRequest GetPersistRequest()
        {
            return new PersistRequest {
                Bucket = Encoding.UTF8.GetBytes("test_bucket"),
                Key = Encoding.UTF8.GetBytes("test_key"),
                ReturnBody = true,
                Content = new RiakDocument {
                    Value = Encoding.UTF8.GetBytes("this is a test")
                }
            };
        }

        public static RiakFindRequest GetFindRequest()
        {
            return new RiakFindRequest {
                Bucket = "test",
                Keys = new string[] { "123" }
            };
        }

        public static DetachRequest GetDetachRequest()
        {
            return new DetachRequest {
                Bucket = "test".GetBytes(),
                Key = "123".GetBytes()
            };
        }

        public static RiakDocument GetTestDocument()
        {
            return new RiakDocument {
                    Value = Encoding.UTF8.GetBytes("this is a test"),
                    ContentType = Encoding.UTF8.GetBytes("text/plain")
                };
        }

        public static RiakDocumentRepository GetConnectionManager()
        {
            var connectionManager = new RiakConnectionManager();
            connectionManager.AddConnection("192.168.30.118", 8087);
            return new RiakDocumentRepository(connectionManager);
        }
    }
}
