using System.Data.RiakClient.Models;

namespace System.Data.RiakClient
{
    public class RiakManagementRepository
    {
        public RiakConnection Connection { get; set; }

        public RiakManagementRepository(RiakConnection connection)
        {
            Connection = connection;
        }

        public RiakResponse<bool> Ping()
        {
            var response = Connection.WriteWithoutRequestBody(false, RequestMethod.Ping);
            if (response.ResponseCode == RiakResponseCode.Successful) response.Result = true;
            return response;
        }

        public RiakResponse<string> FindClientId()
        {
            var r = Connection.WriteWithoutRequestBody(new byte[] {}, RequestMethod.FindClientId);

            return r.ResponseCode == RiakResponseCode.Failed
                ? RiakResponse<string>.WithErrors(r.Messages)
                : RiakResponse<string>.ReadResponse(() => {
                    var response = Connection.Read<FindClientIdResponse>();
                    return response.ResponseCode == RiakResponseCode.Failed
                        ? RiakResponse<string>.WithErrors(r.Messages)
                        : RiakResponse<string>.WithoutErrors(response.Result.ClientId.DecodeToString());
                });
        }

        public RiakResponse<string> PersistClientId(PersistClientIdRequest request)
        {
            var r = Connection.Write(request, RequestMethod.PersistClientId);
            return r.ResponseCode == RiakResponseCode.Failed
                ? RiakResponse<string>.WithErrors(r.Messages)
                : RiakResponse<string>.WithoutErrors(request.ClientId.DecodeToString());
        }

        public RiakResponse<ServerInfo> GetServerInformation()
        {
            var r = Connection.WriteWithoutRequestBody(new byte[] { }, RequestMethod.ServerInfo);

            return r.ResponseCode == RiakResponseCode.Failed
                ? RiakResponse<ServerInfo>.WithErrors(r.Messages)
                : RiakResponse<ServerInfo>.ReadResponse(() => {
                    var response = Connection.Read<GetServerInformationResponse>();
                    return response.ResponseCode == RiakResponseCode.Failed
                               ? RiakResponse<ServerInfo>.WithErrors(r.Messages)
                               : RiakResponse<ServerInfo>.WithoutErrors(new ServerInfo {
                                    Node = response.Result.Node.DecodeToString(),
                                    ServerVersion = response.Result.ServerVersion.DecodeToString()
                                });
                });
        }
    }
}
