using System.Data.RiakClient.Models;
using System.Text;
using System.Data.RiakClient;

namespace riak.net.specs
{
    public class ShouldSaveAndRetrieve
    {
        public void Run()
        {
            var riakDocument = new RiakDocument {
                Value = Encoding.UTF8.GetBytes("this is a test"), 
                ContentType = Encoding.UTF8.GetBytes("text/plain")
            };

            var connection = new RiakConnection("192.168.0.188", 8087);
            var key = connection.Persist(null);

            riakDocument = connection.Find(null);

        }
    }
}
