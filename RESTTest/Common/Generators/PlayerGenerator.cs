using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Registration.Requests;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Common.Generators
{
    public static class PlayerGenerator
    {
        private readonly static RestClient client = new RestClient(CommonConstants.Host.IdentityService);

        private static List<string> Players { get; } = new List<string>();

        public static string Id { get; private set; }

        public static string CreatePlayer(string username, string email, bool isTest = true)
        {
            RestRequest request = new RestRequest(Registration.Constants.Path.Register, Method.POST);
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);

            request.AddJsonBody(new RegisterAccountRequest(
                Registration.Constants.Data.RegisterAccount.CorrectPassword,
                username,
                email,
                Registration.Constants.Data.RegisterAccount.Hand
            ));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            string id = json["records"].ToString();

            if (isTest) Id = id;
            else Players.Add(id);
            return id;
        }

        public static void DeletePlayer(string id)
        {
            RestRequest request = new RestRequest(Registration.Constants.Path.User + $"/{id}", Method.DELETE);
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);

            client.Execute(request);
        }

        public static void DeleteAll()
        {
            foreach (string player in Players) DeletePlayer(player);
            DeletePlayer(Id);
        }
    }
}
