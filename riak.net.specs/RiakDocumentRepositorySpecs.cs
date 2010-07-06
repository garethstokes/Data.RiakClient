using NUnit.Framework;
using System.Data.RiakClient;
using System;
using System.Data.RiakClient.Models;
using riak.net.specs.Helpers;

namespace riak.net.specs
{
    [TestFixture]
    public class RiakDocumentRepositorySpecs
    {
        [Test]
        public void ShouldPersistDocumentWithReturedDocument()
        {
            // Arrange.
            var riakConnection = SpecHelpers.GetConnectionManager();
            
            // Act.
            var response = riakConnection.Persist(request => {
                request.Bucket = "test_bucket";
                request.Key = "test_key";
                request.ReturnBody = true;
                request.Content = new RiakDocument {
                    Value = "this is a test".GetBytes()
                };
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNotNull(response.Result);
        }

        [Test]
        public void ShouldPersistDocumentWithoutReturedDocument()
        {
            // Arrange.
            var riakConnection = SpecHelpers.GetConnectionManager();
           
            // Act.
            var response = riakConnection.Persist(request => {
                request.Bucket = "test_bucket";
                request.Key = "test_key";
                request.ReturnBody = false;
                request.Content = new RiakDocument {
                    Value = "this is a test".GetBytes()
                };
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNull(response.Result);
        }

        [Test]
        public void ShouldFindDocument()
        {
            // Arrange.
            var riakConnection = SpecHelpers.GetConnectionManager();

            var bucket = Guid.NewGuid().ToString();
            var key = Guid.NewGuid().ToString();

            riakConnection.Persist(x => {
                x.Bucket = bucket;
                x.Key = key;
                x.ReturnBody = true;
                x.Write = 1;
                x.DW = 1;
                x.Content = new RiakDocument{
                    ContentType = "text/plain".GetBytes(),
                    Value = "this is a test".GetBytes()
                };
            });

            // Act.
            var response = riakConnection.Find(x => {
                x.ReadValue = 1;
                x.Bucket = bucket;
                x.Keys = new string[] { key };
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNotNull(response.Result);
        }

        [Test]
        public void ShouldFindMultipleDocuments()
        {
            // Arrange.
            var riakConnection = SpecHelpers.GetConnectionManager();

            var bucket = Guid.NewGuid().ToString();
            var key_1= Guid.NewGuid().ToString();
            var key_2= Guid.NewGuid().ToString();

            riakConnection.Persist(x => {
                x.Bucket = bucket;
                x.Key = key_1;
                x.ReturnBody = true;
                x.Write = 1;
                x.DW = 1;
                x.Content = new RiakDocument{
                    ContentType = "text/plain".GetBytes(),
                    Value = "this is a test".GetBytes()
                };
            });

            riakConnection.Persist(x => {
                x.Bucket = bucket;
                x.Key = key_2;
                x.ReturnBody = true;
                x.Write = 1;
                x.DW = 1;
                x.Content = new RiakDocument{
                    ContentType = "text/plain".GetBytes(),
                    Value = "this is a test".GetBytes()
                };
            });

            // Act.
            var response = riakConnection.Find(x => {
                x.ReadValue = 1;
                x.Bucket = bucket;
                x.Keys = new string[] { key_1, key_2 };
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNotNull(response.Result);
        }

        [Test]
        public void ShouldDetachDocument()
        {
            // Arrange.
            var riakConnection = SpecHelpers.GetConnectionManager();

            var bucket = Guid.NewGuid().ToString();
            var key = Guid.NewGuid().ToString();

            riakConnection.Persist(x =>
            {
                x.Bucket = bucket;
                x.Key = key;
                x.ReturnBody = true;
                x.Write = 1;
                x.DW = 1;
                x.Content = new RiakDocument
                {
                    ContentType = "text/plain".GetBytes(),
                    Value = "this is a test".GetBytes()
                };
            });

            // Act.
            var response = riakConnection.Detach(x => {
                x.Bucket = bucket.GetBytes();
                x.Key = key.GetBytes();
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsTrue(response.Result);
        }
    }
}
