using Newtonsoft.Json.Linq;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Registration.Requests;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Common.Generators
{
    public static class PlayerGenerator
    {
        public static string Id { get; set; }

        public static void CreatePlayer(RestClient client)
        {
            RestRequest request = new RestRequest(Registration.Constants.Path.Register, Method.POST);
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);

            request.AddJsonBody(new RegisterAccountRequest(
                Registration.Constants.Data.RegisterAccount.CorrectPassword,
                Registration.Constants.Data.RegisterAccount.TestUsername,
                Registration.Constants.Data.RegisterAccount.TestEmail,
                Registration.Constants.Data.RegisterAccount.Hand
            ));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);
            Id = json["records"].ToString();
        }

        public static void DeletePlayer(RestClient client, string id)
        {
            RestRequest request = new RestRequest(Registration.Constants.Path.User + $"/{id}", Method.DELETE);
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);
            IRestResponse response = new RestResponse();
            while (response.StatusCode != HttpStatusCode.OK) response = client.Execute(request);
        }
    }
}
