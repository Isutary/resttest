using Newtonsoft.Json.Linq;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Game.Requests;
using System;
using System.Linq;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Common.Generators
{
    public static class GameGenerator
    {
        public static string Id { get; set; }

        public static void CreateGame(RestClient client, string code)
        {
            RestRequest request = new RestRequest(Game.Constants.Path.Game, Method.POST);
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);

            string open = DateTime.Now.AddMinutes(3).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(6).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(
                Game.Constants.Data.Game.TestGameCode,
                Game.Constants.Data.Game.Prize,
                Game.Constants.Data.Game.Prize,
                open,
                end,
                CommonConstants.Data.False,
                Game.Constants.Data.Game.NonRecurring
            ));

            client.Execute(request);
            Id = GetGameId(client, code);
        }

        public static string GetGameId(RestClient client, string code)
        {
            RestRequest request = new RestRequest(Game.Constants.Path.Game, Method.GET);
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);

            request.AddParameter("from", DateTime.Now.AddMonths(-1).ToString(CommonConstants.Time.Format));
            request.AddParameter("to", DateTime.Now.AddMonths(1).ToString(CommonConstants.Time.Format));
            request.AddParameter("page", Game.Constants.Query.Page);
            request.AddParameter("pageSize", Game.Constants.Query.PageSize);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            return json["records"]
                .Where(x => x["gameCode"].ToString() == code)
                .Select(x => x)
                .FirstOrDefault()["id"]
                .ToString();
        }

        public static void DeleteGame(RestClient client, string id)
        {
            RestRequest request = new RestRequest(Game.Constants.Path.Game + $"/{id}", Method.DELETE);
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);

            client.Execute(request);
        }
    }
}
