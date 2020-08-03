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
using CommonName = RESTTest.Common.Constants.Name;
using CommonResponse = RESTTest.Common.Constants.Response;

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
            StringAssert.Contains(CommonName.Status, json[CommonName.Records].First().ToString());
            StringAssert.Contains(CommonName.AwardedAt, json[CommonName.Records].First().ToString());
            StringAssert.Contains(CommonName.Position, json[CommonName.Records].First().ToString());
            StringAssert.Contains(CommonName.Type, json[CommonName.Records].First().ToString());
            StringAssert.Contains(CommonName.PlayerId, json[CommonName.Records].First().ToString());
            StringAssert.Contains(CommonName.HostId, json[CommonName.Records].First().ToString());
            StringAssert.Contains(CommonName.Description, json[CommonName.Records].First().ToString());
            StringAssert.Contains(CommonName.Id, json[CommonName.Records].First().ToString());
        }

        [TestCaseSource(typeof(ClaimData), nameof(ClaimData.AlreadyClaimed))]
        public void PrizeTests_Prize_Already_Claimed(string id, string name, string paypal, string phone)
        {
            Init(Constants.Path.Prize, Method.POST);
            request.AddJsonBody(new ClaimPasswordRequest(id, name, paypal, phone));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(Constants.Response.AlreadyClaimed, json[CommonName.Message].ToString());
        }

        [TestCaseSource(typeof(ClaimData), nameof(ClaimData.EmptyClaim))]
        public void PrizeTests_Claim_Cannot_Be_Empty(string id, string name, string paypal, string phone)
        {
            Init(Constants.Path.Prize, Method.POST);
            request.AddJsonBody(new ClaimPasswordRequest(id, name, paypal, phone));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][CommonName.FirstName].First().ToString());
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][CommonName.Paypal].First().ToString());
        }

        [TestCaseSource(typeof(ClaimData), nameof(ClaimData.CorrectClaim))]
        public void PrizeTests_Should_Claim(string id, string name, string paypal, string phone)
        {
            Init(Constants.Path.Prize, Method.POST);
            request.AddJsonBody(new ClaimPasswordRequest(id, name, paypal, phone));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(Constants.Response.Status, json[CommonName.Records][CommonName.Status].ToString());

            WinnerTests winnerTests = new WinnerTests();
            winnerTests.WinnerTests_Should_Change(Winner.Constants.Data.Claim.NotClaimed);
        }

        [TestCaseSource(typeof(ClaimData), nameof(ClaimData.IncorrectClaim))]
        public void PrizeTests_Wrong_Claim_ID(string id, string name, string paypal, string phone)
        {
            Init(Constants.Path.Prize, Method.POST);
            request.AddJsonBody(new ClaimPasswordRequest(id, name, paypal, phone));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotExist, json[CommonName.Message].ToString());
        }
    }
}
