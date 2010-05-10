namespace System.Data.RiakClient.Models
{
    public sealed class RiakResponse<T>
    {
        private RiakResponse(){}

        public string[] Messages { get; internal set; }
        public T Result { get; internal set; }
        public RiakResponseCode ResponseCode { get; internal set; }
        public string VectorClock { get; internal set; }

        public static RiakResponse<T> WithoutErrors(T result)
        {
            return new RiakResponse<T>{
                    ResponseCode = RiakResponseCode.Successful,
                    Result = result
                };
        }

        public static RiakResponse<T> ReadResponse(Func<RiakResponse<T>> action)
        {
            return action();
        }

        public static RiakResponse<T> WithErrors(params string[] messages)
        {
            return new RiakResponse<T> {
                ResponseCode = RiakResponseCode.Successful,
                Messages = messages
            };
        }

        public static RiakResponse<T> WithErrors(T result, params string[] messages)
        {
            return new RiakResponse<T>
            {
                ResponseCode = RiakResponseCode.Successful,
                Messages = messages,
                Result = result
            };
        }
    }
}
