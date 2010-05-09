﻿using NUnit.Framework;
using System.Data.RiakClient;
using System.Data.RiakClient.Helpers;
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
            var connectionManager = RiakConnectionManager.FromConfiguration;
            var riakConnection = new RiakDocumentRepository(connectionManager);
            connectionManager.AddConnection("192.168.0.188", 8087);

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

            string bucket = Guid.NewGuid().ToString();
            string key = Guid.NewGuid().ToString();

            riakConnection.Persist(x => {
                x.Bucket = bucket.GetBytes();
                x.Key = key.GetBytes();
                x.ReturnBody = false;
                x.Content = new RiakDocument{
                    Value = "this is a test".GetBytes()
                };
            });

            // Act.
            var response = riakConnection.Find(x => {
                x.Bucket = bucket.GetBytes();
                x.Key = key.GetBytes();
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
            var response = riakConnection.Detach(x => {
                x.Bucket = "test".GetBytes();
                x.Key = "123".GetBytes();
            });

            // Assert.
            Assert.IsTrue(response.ResponseCode == RiakResponseCode.Successful);
            Assert.IsTrue(response.Result);
        }
    }
}