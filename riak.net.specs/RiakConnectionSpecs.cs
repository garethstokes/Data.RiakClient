using NUnit.Framework;
using System.Data.RiakClient;
using riak.net.specs.Helpers;
using System.Data.RiakClient.Models;

namespace riak.net.specs
{
    [TestFixture]
    public class RiakConnectionSpecs
    {
        [Test]
        public void ShouldPersistDocumentWithReturedDocument()
        {
            // Arrange.
            var riakConnection = new RiakRepository("192.168.0.188", 8087);

            // Act.
            var response = riakConnection.Persist(SpecHelpers.GetPersistRequest());

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNotNull(response.Result);
        }

        [Test]
        public void ShouldPersistDocumentWithoutReturedDocument()
        {
            // Arrange.
            var riakConnection = new RiakRepository("192.168.0.188", 8087);
            var request = SpecHelpers.GetPersistRequest();
            request.ReturnBody = false;

            // Act.
            var response = riakConnection.Persist(request);

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNull(response.Result);
        }

        [Test]
        public void ShouldFindDocument()
        {
            // Arrange.
            var riakConnection = new RiakRepository("192.168.0.188", 8087);

            // Act.
            var response = riakConnection.Find(SpecHelpers.GetFindRequest());

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNotNull(response.Result);
        }

        [Test]
        public void ShouldDetachDocument()
        {
            // Arrange.
            var riakConnection = new RiakRepository("192.168.0.188", 8087);

            // Act.
            var response = riakConnection.Detach(SpecHelpers.GetDetachRequest());

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsTrue(response.Result);
        }
    }
}
