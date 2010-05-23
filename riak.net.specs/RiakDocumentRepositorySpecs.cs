using NUnit.Framework;
using System.Data.RiakClient;
using System;
using System.Data.RiakClient.Models;

namespace riak.net.specs
{
    [TestFixture]
    public class RiakDocumentRepositorySpecs
    {
        [Test]
        public void ShouldPersistDocumentWithReturedDocument()
        {
            // Arrange.
            var connectionManager = new RiakConnectionManager();
            connectionManager.AddConnection("192.168.0.188", 8087);
            var riakConnection = new RiakDocumentRepository(connectionManager);
            
            // Act.
            var response = riakConnection.Persist(request => {
                request.Bucket = "test_bucket".GetBytes();
                request.Key = "test_key".GetBytes();
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
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var riakConnection = new RiakDocumentRepository(connectionManager);
            connectionManager.AddConnection("192.168.0.188", 8087); 
           
            // Act.
            var response = riakConnection.Persist(request => {
                request.Bucket = "test_bucket".GetBytes();
                request.Key = "test_key".GetBytes();
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
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var riakConnection = new RiakDocumentRepository(connectionManager);
            connectionManager.AddConnection("192.168.0.188", 8087);

            var bucket = Guid.NewGuid().ToString();
            var key = Guid.NewGuid().ToString();

            riakConnection.Persist(x => {
                x.Bucket = bucket.GetBytes();
                x.Key = key.GetBytes();
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
                x.Bucket = bucket.GetBytes();
                x.Key = key.GetBytes();
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsNotNull(response.Result);
        }

        [Test]
        public void ShouldFindMultipleDocuments()
        {
            // Arrange.
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var riakConnection = new RiakDocumentRepository(connectionManager);
            connectionManager.AddConnection("192.168.0.188", 8087);

            var bucket = Guid.NewGuid().ToString();
            var key_1= Guid.NewGuid().ToString();
            var key_2= Guid.NewGuid().ToString();

            riakConnection.Persist(x => {
                x.Bucket = bucket.GetBytes();
                x.Key = key_1.GetBytes();
                x.ReturnBody = true;
                x.Write = 1;
                x.DW = 1;
                x.Content = new RiakDocument{
                    ContentType = "text/plain".GetBytes(),
                    Value = "this is a test".GetBytes()
                };
            });

            riakConnection.Persist(x => {
                x.Bucket = bucket.GetBytes();
                x.Key = key_2.GetBytes();
                x.ReturnBody = true;
                x.Write = 1;
                x.DW = 1;
                x.Content = new RiakDocument{
                    ContentType = "text/plain".GetBytes(),
                    Value = "this is a test".GetBytes()
                };
            });

            // Act.
            var response = riakConnection.Find(new [] { key_1, key_2}, x => {
                x.ReadValue = 1;
                x.Bucket = bucket.GetBytes();
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

            var bucket = Guid.NewGuid().ToString();
            var key = Guid.NewGuid().ToString();

            riakConnection.Persist(x =>
            {
                x.Bucket = bucket.GetBytes();
                x.Key = key.GetBytes();
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
