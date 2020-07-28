using Newtonsoft.Json.Linq;
using CommonConstants = RESTTest.Common.Constants;
using RestSharp;

namespace RESTTest.Common.Setup
{
    public class AuthenticationSetupFixture
    {
        private class ClientConfig
        {
            public string Client_id { get; set; } = CommonConstants.Setup.Authentication.Client_id;
            public string Client_secret { get; set; } = CommonConstants.Setup.Authentication.Client_secret;
            public string Response_type { get; set; } = CommonConstants.Setup.Authentication.Response_type;
            public string Scope { get; set; } = CommonConstants.Setup.Authentication.Scope;
            public string Grant_type { get; set; } = CommonConstants.Setup.Authentication.Grant_type;
            public string Username { get; set; } = CommonConstants.Setup.Authentication.Username;
            public string Password { get; set; } = CommonConstants.Setup.Authentication.Password;
        }

        private readonly RestClient client = new RestClient(CommonConstants.Host.IdentityService);
        
        private readonly RestRequest request = new RestRequest(CommonConstants.Setup.Authentication.Path, Method.POST);

        public readonly string token = "Bearer ";

        public AuthenticationSetupFixture()
        {
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddObject(new ClientConfig());
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            token += json["access_token"].ToString();
        }
    }
}
