using NUnit.Framework;
using System.Data.RiakClient;
using riak.net.specs.Helpers;
using System.Data.RiakClient.Models;
using System.Data.RiakClient.Helpers;

namespace riak.net.specs
{
    [TestFixture]
    public class RiakDocumentRepositorySpecs
    {
        [Test]
        public void ShouldPersistDocumentWithReturedDocument()
        {
            // Arrange.
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var riakConnection = new RiakDocumentRepository(connectionManager);
            connectionManager.AddConnection("192.168.0.188", 8087);

            // Act.
            var response = riakConnection.Persist(new PersistRequest {
                Bucket = "test_bucket".GetBytes(),
                Key = "test_key".GetBytes(),
                ReturnBody = true,
                Content = new RiakDocument {
                    Value = "this is a test".GetBytes()
                }
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNotNull(response.Result);
        }

        [Test]
        public void ShouldPersistDocumentWithoutReturedDocument()
        {
            // Arrange.
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var riakConnection = new RiakDocumentRepository(connectionManager);
            connectionManager.AddConnection("192.168.0.188", 8087); 
           
            // Act.
            var response = riakConnection.Persist(new PersistRequest {
                Bucket = "test_bucket".GetBytes(),
                Key = "test_key".GetBytes(),
                ReturnBody = false,
                Content = new RiakDocument {
                    Value = "this is a test".GetBytes()
                }
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNull(response.Result);
        }

        [Test]
        public void ShouldFindDocument()
        {
            // Arrange.
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var riakConnection = new RiakDocumentRepository(connectionManager);
            connectionManager.AddConnection("192.168.0.188", 8087);
            
            // Act.
            var response = riakConnection.Find(new FindRequest {
                Bucket = "test".GetBytes(),
                Key = "123".GetBytes()
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNotNull(response.Result);
        }

        [Test]
        public void ShouldDetachDocument()
        {
            // Arrange.
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var riakConnection = new RiakDocumentRepository(connectionManager);
            connectionManager.AddConnection("192.168.0.188", 8087);
            
            // Act.
            var response = riakConnection.Detach(new DetachRequest {
                Bucket = "test".GetBytes(),
                Key = "123".GetBytes()
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsTrue(response.Result);
        }
    }
}
