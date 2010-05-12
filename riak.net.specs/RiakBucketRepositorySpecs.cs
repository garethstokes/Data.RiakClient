using NUnit.Framework;
using System.Data.RiakClient.Models;
using System.Data.RiakClient;
namespace riak.net.specs
{
    [TestFixture]
    public class RiakBucketRepositorySpecs
    {
        [Test]
        public void ShouldListBuckets()
        {
            // Arrange.
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var repository = new RiakBucketRepository(connectionManager);
            connectionManager.AddConnection("192.168.0.188", 8087);

            // Act.
            var response = repository.ListBuckets();

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsTrue(response.Result.Length > 0);
        }

        [Test]
        [Ignore]
        public void ShouldListKeys()
        {
            // Arrange.
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var repository = new RiakBucketRepository(connectionManager);
            connectionManager.AddConnection("192.168.0.188", 8087);

            // Act.
            var response = repository.ListKeysFor(new ListKeysRequest {Bucket = "test".GetBytes()});

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsTrue(response.Result.Length > 0);
        }
    }
}
