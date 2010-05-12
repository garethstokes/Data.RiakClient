using NUnit.Framework;
using System.Data.RiakClient.Models;
using System.Data.RiakClient;
using System;
namespace riak.net.specs
{
    [TestFixture]
    public class RiakManagementRepositorySpecs
    {
        [Test]
        public void ShouldPing()
        {
            // Arrange.
            var connection = new RiakConnection { Host = "192.168.0.188", Port = 8087 };
            var repository = new RiakManagementRepository(connection);
            
            // Act.
            repository.Ping();
        }

        [Test]
        public void ShouldFindClientId()
        {
            // Arrange.
            var connection = new RiakConnection { Host = "192.168.0.188", Port = 8087 };
            var repository = new RiakManagementRepository(connection);

            // Act.


            // Assert.
            Assert.IsTrue(true);
        }

        [Test]
        public void ShouldPersistClientId()
        {
            // Arrange.
            var connection = new RiakConnection { Host = "192.168.0.188", Port = 8087 };
            var repository = new RiakManagementRepository(connection);

            // Act.
            var response = repository.PersistClientId(new PersistClientIdRequest {ClientId = BitConverter.GetBytes(1337)});

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
        }

        [Test]
        public void ShouldGetServerInformation()
        {
            // Arrange.
            var connection = new RiakConnection { Host = "192.168.0.188", Port = 8087 };
            var repository = new RiakManagementRepository(connection);

            // Act.
            var response = repository.GetServerInformation();

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNotNull(response.Result);
            Assert.IsNotNull(response.Result.Node);
            Assert.IsNotNull(response.Result.ServerVersion);
        }

        [Test]
        [Ignore]
        public void Template()
        {
            // Arrange.

            // Act.

            // Assert.
            Assert.IsTrue(true);
        }

    }
}
