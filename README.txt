var response = riakConnection.Find(x => {
    x.ReadValue = 1;
    x.Bucket = bucket;
    x.Keys = new string[] { key };
});

var document = response.Result;