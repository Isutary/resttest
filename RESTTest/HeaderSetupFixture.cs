using RestSharp;

namespace RESTTest
{
    public class HeaderSetupFixture : AuthenticationSetupFixture
    {
        protected readonly RestClient client;

        protected RestRequest request;

        public HeaderSetupFixture(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }

        public void Init(string resource, Method method)
        {
            request = new RestRequest(resource, method);

            request.AddHeader("x-tenant-id", "rps-live");
            request.AddHeader("Authorization", token);
        }
    }
}
