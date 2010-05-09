using NUnit.Framework;
using System.Data.RiakClient;
using riak.net.specs.Helpers;

namespace riak.net.specs
{
    [TestFixture]
    public class RiakConnectionSpecs
    {
        [Test]
        public void ShouldPackageMessageCorrectly()
        {
            // Arrange.
            var riakConnection = new RiakConnection("", 0);

            // Act.
            var message = riakConnection.PackageMessageFrom(this.GetPersistRequest(), 11);

            // Assert.
            Assert.IsNotNull(message);
            Assert.IsTrue(message.Length == 46);
        }

        [Test]
        public void ShouldPersistDocument()
        {
            // Arrange.
            var riakConnection = new RiakConnection("192.168.0.188", 8087);

            // Act.
            var key = riakConnection.Persist(this.GetPersistRequest());

            // Assert.
            Assert.IsNotNullOrEmpty(key);
        }

        [Test]
        public void ShouldFindDocument()
        {
            // Arrange.
            var riakConnection = new RiakConnection("192.168.0.188", 8087);

            // Act.
            var document = riakConnection.Find(this.GetFindRequest());

            // Assert.
            Assert.True(document.Value.Length > 0);
        }

        [Test]
        public void ShouldDetachDocument()
        {
            // Arrange.
            var riakConnection = new RiakConnection("192.168.0.188", 8087);

            // Act.
            var response = riakConnection.Detach(this.GetDetachRequest());

            // Assert.
            Assert.IsTrue(response);
        }
    }
}
