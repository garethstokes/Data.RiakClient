First you need to establish a connection to riak, and to establish a connection 
to riak you will need to build a RiakConnectionManager

var connectionManager = RiakConnectionManager.FromConfiguration; // doesn't do anything now, IoC coming soon!
connectionManager.AddConnection("192.168.0.188", 8087); // server address and protocol buffer port

then make the connection to riak

var riakConnection = new RiakDocumentRepository(connectionManager);


#Insert




var response = riakConnection.Find(x => {
    x.ReadValue = 1;
    x.Bucket = bucket;
    x.Keys = new string[] { key };
});

var document = response.Result;