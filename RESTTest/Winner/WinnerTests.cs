using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Winner.Requests;
using RESTTest.Winner.TestData;
using System;
using System.Linq;
using System.Net;
using PrizeData = RESTTest.Prize.Constants.Data.Claim;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Winner
{
    public class WinnerTests : HeaderSetupFixture
    {
        public WinnerTests() : base(CommonConstants.Host.GameService) { }

        [TestCaseSource(typeof(ClaimData), nameof(ClaimData.CorrectClaim))]
        public void WinnerTests_Should_Change(string status)
        {
            Init(Constants.Path.Prize, Method.PUT);
            request.AddJsonBody(new ClaimRequest(status));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void WinnerTests_GetWinnerDetails()
        {
            Init(Constants.Path.Prize, Method.GET);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(PrizeData.Username, json["records"]["firstName"].ToString());
            StringAssert.AreEqualIgnoringCase(PrizeData.Email, json["records"]["paypalAddress"].ToString());
            StringAssert.AreEqualIgnoringCase(PrizeData.Phone, json["records"]["phoneNumber"].ToString());
            StringAssert.AreEqualIgnoringCase(PrizeData.Username, json["records"]["userName"].ToString());
            StringAssert.AreEqualIgnoringCase(PrizeData.CorrectId, json["records"]["claimRequestId"].ToString());
            StringAssert.AreEqualIgnoringCase("1st", json["records"]["prizePosition"].ToString());
            StringAssert.AreEqualIgnoringCase("RESTTest Prize", json["records"]["prizeDescription"].ToString());
            StringAssert.AreEqualIgnoringCase("Not claimed", json["records"]["status"].ToString());
            StringAssert.AreEqualIgnoringCase("Game prize", json["records"]["prizeType"].ToString());
        }

        [Test]
        public void WinnerTests_GetWinners()
        {
            Init(Constants.Path.Winner, Method.GET);
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
    }
}
