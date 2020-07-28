using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Game.Requests;
using RESTTest.Game.TestData;
using System;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Game
{
    public class GameTests : HeaderSetupFixture
    {
        public GameTests() : base(CommonConstants.Host.GameService) { }

        [TestCaseSource(typeof(GameData), nameof(GameData.EmptyGameCode))]
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
    }
}
