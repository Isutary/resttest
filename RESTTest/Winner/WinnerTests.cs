﻿using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Winner.Requests;
using RESTTest.Winner.TestData;
using System;
using System.Linq;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Winner
{
    public class WinnerTests : HeaderSetupFixture
    {
        public WinnerTests() : base(CommonConstants.RestClient.GameService) { }

        [TestCaseSource(typeof(ClaimData), nameof(ClaimData.CorrectClaim))]
        public void WinnerTests_Should_Change(string status)
        {
            Init(Constants.Prize, Method.PUT);
            request.AddJsonBody(new ClaimRequest(status));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void WinnerTests_GetWinnerDetails()
        {
            Init(Constants.Prize, Method.GET);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase("Salt", json["records"]["firstName"].ToString());
            StringAssert.AreEqualIgnoringCase("salt@paypal.com", json["records"]["paypalAddress"].ToString());
            StringAssert.AreEqualIgnoringCase("+123456", json["records"]["phoneNumber"].ToString());
            StringAssert.AreEqualIgnoringCase("Salt", json["records"]["userName"].ToString());
            StringAssert.AreEqualIgnoringCase("91659763-da76-45da-29d9-08d82fe6b8d8", json["records"]["claimRequestId"].ToString());
            StringAssert.AreEqualIgnoringCase("1st", json["records"]["prizePosition"].ToString());
            StringAssert.AreEqualIgnoringCase("RESTTest Prize", json["records"]["prizeDescription"].ToString());
            StringAssert.AreEqualIgnoringCase("Not claimed", json["records"]["status"].ToString());
            StringAssert.AreEqualIgnoringCase("Game prize", json["records"]["prizeType"].ToString());
        }

        [Test]
        public void WinnerTests_GetWinners()
        {
            Init(Constants.Winner, Method.GET);
            request.AddParameter("from", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-ddTHH:mm:ssZ"));
            request.AddParameter("to", DateTime.Now.AddMonths(1).ToString("yyyy-MM-ddTHH:mm:ssZ"));
            request.AddParameter("page", Constants.Page);
            request.AddParameter("pageSize", Constants.PageSize);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(Constants.PageSize, json["records"].Count().ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Page, json["paginationInfo"]["page"].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.PageSize, json["paginationInfo"]["pageSize"].ToString());
        }
    }
}