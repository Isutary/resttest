using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Winner.Requests;
using RESTTest.Winner.TestData;
using System;
using System.Linq;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;
using CommonName = RESTTest.Common.Constants.Name;

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
            StringAssert.AreEqualIgnoringCase(Constants.Response.Username, json[CommonName.Records][CommonName.FirstName].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.Email, json[CommonName.Records][CommonName.Paypal].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.Phone, json[CommonName.Records][CommonName.Phone].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.Username, json[CommonName.Records][CommonName.Username].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.CorrectId, json[CommonName.Records][CommonName.ClaimId].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.Position, json[CommonName.Records][CommonName.Position].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.Prize, json[CommonName.Records][CommonName.Description].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.Status, json[CommonName.Records][CommonName.Status].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.Type, json[CommonName.Records][CommonName.Type].ToString());
        }

        [Test]
        public void WinnerTests_GetWinners()
        {
            Init(Constants.Path.Winner, Method.GET);
            request.AddParameter(CommonConstants.Query.From, DateTime.Now.AddMonths(-1).ToString(CommonConstants.Time.Format));
            request.AddParameter(CommonConstants.Query.To, DateTime.Now.AddMonths(1).ToString(CommonConstants.Time.Format));
            request.AddParameter(CommonConstants.Query.Page, Constants.Query.Page);
            request.AddParameter(CommonConstants.Query.PageSize, Constants.Query.PageSize);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(Constants.Response.PageSize, json[CommonName.Records].Count().ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.Page, json[CommonName.Info][CommonName.Page].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.PageSize, json[CommonName.Info][CommonName.PageSize].ToString());
        }
    }
}
