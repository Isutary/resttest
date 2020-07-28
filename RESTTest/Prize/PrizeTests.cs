using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Prize.Requests;
using RESTTest.Prize.TestData;
using RESTTest.Winner;
using System.Linq;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Prize
{
    public class PrizeTests : HeaderSetupFixture
    {
        public PrizeTests() : base(CommonConstants.Host.GameService) { }

        [Test]
        public void PrizeTests_GetUnclaimedPrizes()
        {
            Init(Constants.Path.Prize, Method.GET);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.Contains("status", string.Join(" ", json["records"].First().ToString()));
            StringAssert.Contains("awardedAt", string.Join(" ", json["records"].First().ToString()));
            StringAssert.Contains("prizePosition", string.Join(" ", json["records"].First().ToString()));
            StringAssert.Contains("prizeType", string.Join(" ", json["records"].First().ToString()));
            StringAssert.Contains("playerId", string.Join(" ", json["records"].First().ToString()));
            StringAssert.Contains("hostId", string.Join(" ", json["records"].First().ToString()));
            StringAssert.Contains("prizeDescription", string.Join(" ", json["records"].First().ToString()));
            StringAssert.Contains("id", string.Join(" ", json["records"].First().ToString()));
        }

        [TestCaseSource(typeof(ClaimData), nameof(ClaimData.AlreadyClaimed))]
        public void PrizeTests_Prize_Already_Claimed(string id, string name, string paypal, string phone)
        {
            Init(Constants.Path.Prize, Method.POST);
            request.AddJsonBody(new ClaimPasswordRequest(id, name, paypal, phone));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("Claim request can not be updated for already claimed prizes", json["message"].ToString());
        }

        [TestCaseSource(typeof(ClaimData), nameof(ClaimData.EmptyClaim))]
        public void PrizeTests_Claim_Cannot_Be_Empty(string id, string name, string paypal, string phone)
        {
            Init(Constants.Path.Prize, Method.POST);
            request.AddJsonBody(new ClaimPasswordRequest(id, name, paypal, phone));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("must not be empty", json["errors"]["firstName"].First().ToString());
            StringAssert.Contains("must not be empty", json["errors"]["paypalAddress"].First().ToString());
        }

        [TestCaseSource(typeof(ClaimData), nameof(ClaimData.CorrectClaim))]
        public void PrizeTests_Should_Claim(string id, string name, string paypal, string phone)
        {
            Init(Constants.Path.Prize, Method.POST);
            request.AddJsonBody(new ClaimPasswordRequest(id, name, paypal, phone));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase("Pending", json["records"]["status"].ToString());

            WinnerTests winnerTests = new WinnerTests();
            winnerTests.WinnerTests_Should_Change("Not claimed");
        }

        [TestCaseSource(typeof(ClaimData), nameof(ClaimData.IncorrectClaim))]
        public void PrizeTests_Wrong_Claim_ID(string id, string name, string paypal, string phone)
        {
            Init(Constants.Path.Prize, Method.POST);
            request.AddJsonBody(new ClaimPasswordRequest(id, name, paypal, phone));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            StringAssert.Contains("does not exists as winner for game Id", json["message"].ToString());
        }
    }
}
