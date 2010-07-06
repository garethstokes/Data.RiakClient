using NUnit.Framework;
using System.Data.RiakClient.Models;
using System.Data.RiakClient;
using System;
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
            connectionManager.AddConnection("192.168.30.118", 8087);

            // Act.
            var response = repository.ListBuckets();

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
        }

        [Test]
        public void ShouldListKeys()
        {
            // Arrange.
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var repository = new RiakBucketRepository(connectionManager);
            var documentRepository = new RiakDocumentRepository(connectionManager);
            connectionManager.AddConnection("192.168.30.118", 8087);

            for (var i = 0; i < 3; i++ )
            {
                documentRepository.Persist(x =>
                {
                    x.Bucket = "test_bucket".GetBytes();
                    x.Key = string.Format("test_key_{0}", i).GetBytes();
                    x.ReturnBody = true;
                    x.Write = 1;
                    x.DW = 1;
                    x.Content = new RiakDocument
                    {
                        ContentType = "text/plain".GetBytes(),
                        Value = "this is a test".GetBytes()
                    };
                });   
            }

            // Act.
            var response = repository.ListKeysFor(new ListKeysRequest {Bucket = "test_bucket".GetBytes()});

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Console.WriteLine("response count: " + response.Result.Length);
            Assert.IsTrue(response.Result.Length > 0);
        }
    }
}
