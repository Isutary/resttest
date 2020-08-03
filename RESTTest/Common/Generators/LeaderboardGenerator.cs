using Newtonsoft.Json.Linq;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Leaderboard.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonConstants = RESTTest.Common.Constants;
using CommonName = RESTTest.Common.Constants.Name;
using SetupName = RESTTest.Common.Constants.Setup.Name;

namespace RESTTest.Common.Generators
{
    public static class LeaderboardGenerator
    {
        private readonly static RestClient client = new RestClient(CommonConstants.Host.LeaderboardService);

        private static List<string> Leaderboards { get; } = new List<string>();

        public static string Id { get; private set; }

        public static string CreateLeaderboard(string name, bool isTest = true)
        {
            RestRequest request = new RestRequest(Leaderboard.Constants.Path.Leaderboard, Method.POST);
            request.AddHeader(SetupName.X_tenant_id, CommonConstants.Setup.X_tenant_id);
            request.AddHeader(SetupName.Authorization, AuthenticationSetupFixture.token);
            request.AddJsonBody(new CreateLeaderboardRequest(name));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);
            
            string id = json[CommonName.Records][CommonName.Id].ToString();
            if (isTest) Id = id;
            else Leaderboards.Add(id);

            return id;
        }

        public static void DeleteLeaderboard(string id)
        {
            RestRequest request = new RestRequest(Leaderboard.Constants.Path.Leaderboard + $"/{id}", Method.DELETE);
            request.AddHeader(SetupName.X_tenant_id, CommonConstants.Setup.X_tenant_id);
            request.AddHeader(SetupName.Authorization, AuthenticationSetupFixture.token);

            client.Execute(request);
        }

        public static void DeleteAll()
        {
            Parallel.ForEach(Leaderboards, leaderboard => DeleteLeaderboard(leaderboard));
            DeleteLeaderboard(Id);
        }
    }
}
