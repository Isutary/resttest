using Newtonsoft.Json.Linq;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Game.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using CommonConstants = RESTTest.Common.Constants;
using CommonName = RESTTest.Common.Constants.Name;
using SetupName = RESTTest.Common.Constants.Setup.Name;

namespace RESTTest.Common.Generators
{
    public static class GameGenerator
    {
        private readonly static RestClient client = new RestClient(CommonConstants.Host.GameService);

        private static List<string> Games { get; } = new List<string>();

        public static string Id { get; private set; }

        public static void CreateGame(string code, bool isTest = true)
        {
            RestRequest request = new RestRequest(Game.Constants.Path.Game, Method.POST);
            request.AddHeader(SetupName.X_tenant_id, CommonConstants.Setup.X_tenant_id);
            request.AddHeader(SetupName.Authorization, AuthenticationSetupFixture.token);

            string open = DateTime.Now.AddMinutes(3).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(6).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(
                code,
                Game.Constants.Data.Game.Prize,
                Game.Constants.Data.Game.Prize,
                open,
                end,
                CommonConstants.Data.False,
                Game.Constants.Data.Game.NonRecurring
            ));

            client.Execute(request);
            string id = GetGameId(code);
            if (isTest) Id = id;
            else Games.Add(id);
        }

        public static string GetGameId(string code)
        {
            RestRequest request = new RestRequest(Game.Constants.Path.Game, Method.GET);
            request.AddHeader(SetupName.X_tenant_id, CommonConstants.Setup.X_tenant_id);
            request.AddHeader(SetupName.Authorization, AuthenticationSetupFixture.token);

            request.AddParameter(CommonConstants.Query.From, DateTime.Now.AddMonths(-1).ToString(CommonConstants.Time.Format));
            request.AddParameter(CommonConstants.Query.To, DateTime.Now.AddMonths(1).ToString(CommonConstants.Time.Format));
            request.AddParameter(CommonConstants.Query.Page, Game.Constants.Query.Page);
            request.AddParameter(CommonConstants.Query.PageSize, Game.Constants.Query.PageSize);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            return json[CommonName.Records]
                .Where(x => x[Game.Constants.Name.Code].ToString() == code)
                .Select(x => x)
                .FirstOrDefault()[CommonName.Id]
                .ToString();
        }

        public static void DeleteGame(string id)
        {
            RestRequest request = new RestRequest(Game.Constants.Path.Game + $"/{id}", Method.DELETE);
            request.AddHeader(SetupName.X_tenant_id, CommonConstants.Setup.X_tenant_id);
            request.AddHeader(SetupName.Authorization, AuthenticationSetupFixture.token);

            client.Execute(request);
        }

        public static void DeleteAll()
        {
            foreach (string game in Games) DeleteGame(game);
            DeleteGame(Id);
        }
    }
}
