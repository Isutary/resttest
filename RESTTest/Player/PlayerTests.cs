﻿using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Player.Requests;
using RESTTest.Player.TestData;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;
using CommonName = RESTTest.Common.Constants.Name;
using CommonResponse = RESTTest.Common.Constants.Response;

namespace RESTTest.Player
{
    public class PlayerTests : HeaderSetupFixture
    {
        public PlayerTests() : base(CommonConstants.Host.GameService) { }

        [TestCaseSource(typeof(GoldenLivesData), nameof(GoldenLivesData.NegativeValue))]
        public void PlayerTests_Negative_Number_Of_Lives(string lives)
        {
            Init(Constants.Path.GoldenLives, Method.PUT);
            request.AddJsonBody(new GoldenLivesRequest(lives));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(Constants.Response.GoldenLivesRule, json[CommonName.Message].ToString());
        }

        [TestCaseSource(typeof(SettingsData), nameof(SettingsData.IncorrectColor))]
        public void PlayerTests_Incorrect_Hand_Color(string hand, string amp, string apn, string sound)
        {
            Init(Constants.Path.Settings, Method.PUT);
            request.AddJsonBody(new UpdateSettingsRequest(hand, amp, apn, sound));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotExist, json[CommonName.Message].ToString());
        }

        [TestCaseSource(typeof(SettingsData), nameof(SettingsData.CorrectColor))]
        public void PlayerTests_Correct_Hand_Color(string hand, string amp, string apn, string sound)
        {
            Init(Constants.Path.Settings, Method.PUT);
            request.AddJsonBody(new UpdateSettingsRequest(hand, amp, apn, sound));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
