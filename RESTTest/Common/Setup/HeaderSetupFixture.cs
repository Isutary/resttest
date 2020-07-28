using RestSharp;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Common.Setup
{
    public class HeaderSetupFixture
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

            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);
        }
    }
}
