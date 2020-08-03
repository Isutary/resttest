using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Generators;
using RESTTest.Common.Setup;
using RESTTest.Game.Requests;
using RESTTest.Game.TestData;
using System;
using System.Linq;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;
using CommonResponse = RESTTest.Common.Constants.Response;
using CommonName = RESTTest.Common.Constants.Name;

namespace RESTTest.Game
{
    public class GameTests : HeaderSetupFixture
    {
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
            StringAssert.Contains(CommonResponse.NotNullOrEmpty, json[CommonName.Message].ToString());
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
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.First].First.ToString());
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
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.Consolation].First.ToString());
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
            StringAssert.Contains(Constants.Response.GreaterThan, json[CommonName.Errors][Constants.Name.EndAt].First.ToString());
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
            StringAssert.Contains(Constants.Response.MustBeSame, json[CommonName.Message].ToString());
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
            StringAssert.Contains(CommonResponse.NotExist, json[CommonName.Message].ToString());
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
            StringAssert.Contains(Constants.Response.Exists, json[CommonName.Message].ToString());
        }

        [Test]
        public void GameTests_GetUpcomingGames()
        {
            Init(Constants.Path.Game, Method.GET);
            request.AddParameter(CommonConstants.Query.From, DateTime.Now.AddMonths(-1).ToString(CommonConstants.Time.Format));
            request.AddParameter(CommonConstants.Query.To, DateTime.Now.AddMonths(1).ToString(CommonConstants.Time.Format));
            request.AddParameter(CommonConstants.Query.Page, Constants.Query.Page);
            request.AddParameter(CommonConstants.Query.PageSize, Constants.Query.PageSize);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(Constants.Query.PageSize, json[CommonName.Records].Count().ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Query.Page, json[CommonName.Info][CommonName.Page].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Query.PageSize, json[CommonName.Info][CommonName.PageSize].ToString());
        }

        [TestCaseSource(typeof(GameData), nameof(GameData.CorrectInformation))]
        public void GameTests_Should_Create_Game(string code, string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game, Method.POST);
            string open = DateTime.Now.AddMinutes(3).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(6).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new AddGameRequest(code, first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            GameGenerator.DeleteGame(GameGenerator.GetGameId(code));
        }

        [Test]
        public void GameTests_GetGameById()
        {
            Init(Constants.Path.Game + $"/{GameGenerator.Id}", Method.GET);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(Constants.Data.Game.TestGameCode, json[CommonName.Records][Constants.Name.Code].ToString());
        }

        [TestCaseSource(typeof(UpdateData), nameof(UpdateData.EmptyFirstPrize))]
        public void GameTests_Empty_First_Prize(string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game + $"/{GameGenerator.Id}", Method.PUT);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(8).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new UpdateGameRequest(first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.First].First.ToString());
        }

        [TestCaseSource(typeof(UpdateData), nameof(UpdateData.EmptyConsolationPrize))]
        public void GameTests_Empty_Consolation_Prize(string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game + $"/{GameGenerator.Id}", Method.PUT);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(8).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new UpdateGameRequest(first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.Consolation].First.ToString());
        }

        [TestCaseSource(typeof(UpdateData), nameof(UpdateData.CorrectInformation))]
        public void GameTests_Game_Should_Update(string first, string consolation, string recurring, string pattern)
        {
            Init(Constants.Path.Game + $"/{GameGenerator.Id}", Method.PUT);
            string open = DateTime.Now.AddMinutes(5).ToString(CommonConstants.Time.Format);
            string end = DateTime.Now.AddMinutes(8).ToString(CommonConstants.Time.Format);
            request.AddJsonBody(new UpdateGameRequest(first, consolation, open, end, recurring, pattern));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase(Constants.Data.Game.CorrectCode)]
        public void GameTests_Delete_Should_Work(string code)
        {
            GameGenerator.CreateGame(code, false);

            Init(Constants.Path.Game + $"/{GameGenerator.GetGameId(code)}", Method.DELETE);

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
            StringAssert.Contains(Constants.Name.NextGameDate, json.ToString());
            StringAssert.Contains(Constants.Name.NextGameId, json.ToString());
            StringAssert.Contains(Constants.Name.NextGamePrizeDescription, json.ToString());
            StringAssert.Contains(Constants.Name.NextGameConsolationPrizeDescription, json.ToString());
            StringAssert.Contains(Constants.Name.PreviousWinner, json.ToString());
            StringAssert.Contains(Constants.Name.PreviousGamePrizeDescription, json.ToString());
            StringAssert.Contains(Constants.Name.PreviousGameConsolationPrizeDescription, json.ToString());
        }
    }
}
