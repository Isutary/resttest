using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Game.Requests;
using RESTTest.Game.TestData;
using System;
using System.Linq;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Game
{
    public class GameTests : HeaderSetupFixture
    {
        private string GameId;
        private void GetTestGameID(string code)
        {
            Init(Constants.Path.Game, Method.GET);
            request.AddParameter("from", DateTime.Now.AddMonths(-1).ToString(CommonConstants.Time.Format));
            request.AddParameter("to", DateTime.Now.AddMonths(1).ToString(CommonConstants.Time.Format));
            request.AddParameter("page", Constants.Query.Page);
            request.AddParameter("pageSize", Constants.Query.PageSize);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            GameId = json["records"]
                .Where(x => x["gameCode"].ToString() == code)
                .Select(x => x)
                .FirstOrDefault()["id"]
                .ToString();
        }
        public GameTests() : base(CommonConstants.Host.GameService) { }

        [TestCaseSource(typeof(GameData), nameof(GameData.IncorrectGameCode))]
        public void GameTests_Game_Code_Cannot_Be_Empty(string code, string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game, Method.POST);
            string open = DateTime.Now.AddMinutes(1).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(1).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(code, first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            StringAssert.Contains("code cannot be null or empty", json["message"].ToString());
        }

        [TestCaseSource(typeof(GameData), nameof(GameData.EmptyFirstPrize))]
        public void GameTests_First_Prize_Cannot_Be_Empty(string code, string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game, Method.POST);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(8).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(code, first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("must not be empty", json["errors"]["firstPrize"].First.ToString());
        }

        [TestCaseSource(typeof(GameData), nameof(GameData.EmptyConsolationPrize))]
        public void GameTests_Consolation_Prize_Cannot_Be_Empty(string code, string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game, Method.POST);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(8).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(code, first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("must not be empty", json["errors"]["consolationPrize"].First.ToString());
        }

        [TestCaseSource(typeof(GameData), nameof(GameData.CorrectInformation))]
        public void GameTests_End_Must_Be_Greater_Than_Start(string code, string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game, Method.POST);
            string open = DateTime.Now.AddMinutes(-5).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(code, first, consolation, open, open, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("must be greater than", json["errors"]["endAt"].First.ToString());
        }

        [TestCaseSource(typeof(GameData), nameof(GameData.CorrectInformation))]
        public void GameTests_Start_And_End_Must_Be_Same(string code, string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game, Method.POST);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(15).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(code, first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            StringAssert.Contains("StartAt and EndAt must be the same", json["message"].ToString());
        }

        [TestCaseSource(typeof(GameData), nameof(GameData.IncorrectRecurring))]
        public void GameTests_Incorrect_Recurring(string code, string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game, Method.POST);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(code, first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            StringAssert.Contains("game recurring pattern does not exist", json["message"].ToString());
        }

        [TestCaseSource(typeof(GameData), nameof(GameData.TakenGameCode))]
        public void GameTests_GameCode_Already_Exists(string code, string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game, Method.POST);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(8).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(code, first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("already exits", json["message"].ToString());
        }

        [Test]
        public void GameTests_GetUpcomingGames()
        {
            Init(Constants.Path.Game, Method.GET);
            request.AddParameter("from", DateTime.Now.AddMonths(-1).ToString(CommonConstants.Time.Format));
            request.AddParameter("to", DateTime.Now.AddMonths(1).ToString(CommonConstants.Time.Format));
            request.AddParameter("page", Constants.Query.Page);
            request.AddParameter("pageSize", Constants.Query.PageSize);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(Constants.Query.PageSize, json["records"].Count().ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Query.Page, json["paginationInfo"]["page"].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Query.PageSize, json["paginationInfo"]["pageSize"].ToString());
        }

        [TestCaseSource(typeof(GameData), nameof(GameData.CorrectInformation))]
        [Order(1)]
        public void GameTests_Should_Create_Game(string code, string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game, Method.POST);
            string open = DateTime.Now.AddMinutes(3).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(6).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(code, first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            request.Parameters.RemoveAll(x => x.Type == ParameterType.RequestBody);
            GetTestGameID(code);
        }

        [Test]
        [Order(2)]
        public void GameTests_GetGameById()
        {
            Init(Constants.Path.Game + $"/{GameId}", Method.GET);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(Constants.Data.Game.CorrectCode, json["records"]["gameCode"].ToString());
        }

        [TestCaseSource(typeof(UpdateData), nameof(UpdateData.EmptyFirstPrize))]
        [Order(2)]
        public void GameTests_Empty_First_Prize(string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game + $"/{GameId}", Method.PUT);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(8).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new UpdateGameRequest(first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("must not be empty", json["errors"]["firstPrize"].First.ToString());
        }

        [TestCaseSource(typeof(UpdateData), nameof(UpdateData.EmptyConsolationPrize))]
        [Order(2)]
        public void GameTests_Empty_Consolation_Prize(string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game + $"/{GameId}", Method.PUT);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(8).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new UpdateGameRequest(first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("must not be empty", json["errors"]["consolationPrize"].First.ToString());
        }

        [TestCaseSource(typeof(UpdateData), nameof(UpdateData.CorrectInformation))]
        [Order(2)]
        public void GameTests_Game_Should_Update(string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game + $"/{GameId}", Method.PUT);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(8).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new UpdateGameRequest(first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [Order(3)]
        public void GameTests_Delete_Should_Work()
        {
            Init(Constants.Path.Game + $"/{GameId}", Method.DELETE);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void GameTests_GetNextGameInfo()
        {
            Init(Constants.Path.NextGame, Method.GET);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.Contains("nextGameDate", json.ToString());
            StringAssert.Contains("nextGameId", json.ToString());
            StringAssert.Contains("nextGamePrizeDescription", json.ToString());
            StringAssert.Contains("nextGameConsolationPrizeDescription", json.ToString());
            StringAssert.Contains("previousWinner", json.ToString());
            StringAssert.Contains("previousGamePrizeDescription", json.ToString());
            StringAssert.Contains("previousGameConsolationPrizeDescription", json.ToString());
        }
    }
}
