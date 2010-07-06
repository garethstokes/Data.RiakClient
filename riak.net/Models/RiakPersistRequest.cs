using System.Linq;
using System.Data.RiakClient.Models;

namespace System.Data.RiakClient
{
    public class RiakPersistRequest
    {
		public RiakPersistRequest() {
			Write = 2; 
			DW = 2;
			ReturnBody = true;
		}
		
        public string Bucket { get; set; }
        public string Key { get; set; }
        public string VClock { get; set; }

		public RiakDocument Content { get; set; }

        public uint Write { get; set; }
        public uint DW { get; set; }
        public bool ReturnBody { get; set; }
		
		internal PersistRequest ProxyRequest()
		{
			return new PersistRequest {
				Bucket = Bucket.GetBytes(),
				Key = Key.GetBytes(),
				VClock = (VClock == null) ? null : VClock.GetBytes(),
				Content = Content, 
				Write = Write, 
				DW = DW, 
				ReturnBody = ReturnBody 
			};
		}
    }
}
