using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace RESTTest
{
    public class AuthenticationSetupFixture
    {
        private class ClientConfig
        {
            public string client_id { get; set; } = "rps-live-mobile-client";
            public string client_secret { get; set; } = "39EEE5EC46AF48A6836C65EA636D1C5A";
            public string response_type { get; set; } = "code id_token";
            public string scope { get; set; } = "openid profile email aaf-notification-service-api aaf-leaderboard-service-api aaf-reporting-service-api aaf-tenant-service-api aaf-game-service-api aaf-audit-service-api aaf-identity-service-api aaf-supporting-service-api offline_access";
            public string grant_type { get; set; } = "password";
            public string Username { get; set; } = "salt@test.com";
            public string Password { get; set; } = "Plavi.12.";
        }

        private readonly RestClient client = new RestClient("https://identity-service.dev.rps-live.applicita.com");
        
        private readonly RestRequest request = new RestRequest("connect/token", Method.POST);

        public readonly string token = "Bearer ";

        public AuthenticationSetupFixture()
        {
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded").AddObject(new ClientConfig());
            request.AddHeader("x-tenant-id", "rps-live");

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            token += json["access_token"].ToString();
        }
    }
}
