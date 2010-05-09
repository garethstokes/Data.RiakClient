using NUnit.Framework;
using System.Data.RiakClient.Models;
using riak.net.specs.Helpers;
namespace riak.net.specs
{
    [TestFixture]
    public class PackagedMessageSpecs
    {
        [Test]
        public void ShouldPackageMessageCorrectly()
        {
            // Act.
            var message = PackagedMessage.From(SpecHelpers.GetPersistRequest(), RequestMethod.Perist);

            // Assert.
            Assert.IsNotNull(message);
            Assert.IsTrue(message.Length == 48);
        }
    }
}
