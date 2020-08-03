using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Generators;
using RESTTest.Common.Setup;
using RESTTest.Leaderboard.Helper;
using RESTTest.Leaderboard.Requests;
using RESTTest.Leaderboard.TestData;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;
using CommonName = RESTTest.Common.Constants.Name;
using CommonResponse = RESTTest.Common.Constants.Response;

namespace RESTTest.Leaderboard
{
    public class LeaderboardTests : HeaderSetupFixture
    {
        public LeaderboardTests() : base(CommonConstants.Host.LeaderboardService) { }

        [TestCaseSource(typeof(LeaderboardData), nameof(LeaderboardData.CorrectName))]
        public void LeaderboardTests_Should_Create_Leaderboard(string name)
        {
            Init(Constants.Path.Leaderboard, Method.POST);
            request.AddJsonBody(new CreateLeaderboardRequest(name));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(name, json[CommonName.Records][Constants.Name.LeaderboardName].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.Type, json[CommonName.Records][Constants.Name.Type].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.CreatedById, json[CommonName.Records][Constants.Name.CreatedById].ToString());

            string id = json[CommonName.Records][CommonName.Id].ToString();
            LeaderboardGenerator.DeleteLeaderboard(id);
        }

        [TestCaseSource(typeof(LeaderboardData), nameof(LeaderboardData.EmptyName))]
        public void LeaderboardTests_Empty_Leaderboard_Name(string name)
        {
            Init(Constants.Path.Leaderboard, Method.POST);
            request.AddJsonBody(new CreateLeaderboardRequest(name));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.LeaderboardName].First.ToString());
        }

        [TestCaseSource(typeof(LeaderboardData), nameof(LeaderboardData.LongName))]
        public void LeaderboardTests_Too_Long_Leaderboard_Name(string name)
        {
            Init(Constants.Path.Leaderboard, Method.POST);
            request.AddJsonBody(new CreateLeaderboardRequest(name));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(Constants.Response.TooLong, json[CommonName.Errors][Constants.Name.LeaderboardName].First.ToString());
        }

        [TestCaseSource(typeof(SubscribeData), nameof(SubscribeData.EmtpyPin))]
        public void LeaderboardTests_Pin_Cannot_Be_Empty(string pin)
        {
            Init(Constants.Path.Subscribe, Method.POST);
            request.AddJsonBody(new SubscribeRequest(pin));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotNullOrEmpty, json[CommonName.Message].ToString());
        }

        [TestCaseSource(typeof(SubscribeData), nameof(SubscribeData.IncorrectPin))]
        public void LeaderboardTests_Incorrect_Pin(string pin)
        {
            Init(Constants.Path.Subscribe, Method.POST);
            request.AddJsonBody(new SubscribeRequest(pin));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotValid, json[CommonName.Message].ToString());
        }

        [TestCaseSource(typeof(SubscribeData), nameof(SubscribeData.AlreadySubscribed))]
        public void LeaderboardTests_Already_Subscribed(string pin)
        {
            Init(Constants.Path.Subscribe, Method.POST);
            request.AddJsonBody(new SubscribeRequest(pin));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(Constants.Response.AlreadySubscribed, json[CommonName.Message].ToString());
        }

        [TestCaseSource(typeof(UpdateGlobalPrizeData), nameof(UpdateGlobalPrizeData.EmptyPrize))]
        public void LeaderboardTests_Prize_Cannot_Be_Emtpy(string currentId, string current, string futureId, string future)
        {
            Init(Constants.Path.GlobalPrize, Method.PUT);
            request.AddJsonBody(new UpdateGlobalPrizeRequest(currentId, current, futureId, future));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotNullOrEmpty, json[CommonName.Message].ToString());
        }

        [TestCaseSource(typeof(UpdateGlobalPrizeData), nameof(UpdateGlobalPrizeData.CorrectPrize))]
        public void LeaderboardTests_Prize_Should_Change(string current, string future)
        {
            Init(Constants.Path.GlobalPrize, Method.PUT);
            string CurrentId = Extractor.CurrentPrizeId;
            string FutureId = Extractor.FuturePrizeId;
            request.AddJsonBody(new UpdateGlobalPrizeRequest(CurrentId, current, FutureId, future));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            Extractor.Revert();
        }
    }
}
