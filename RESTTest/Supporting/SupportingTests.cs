using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Supporting.Requests;
using RESTTest.Supporting.TestData;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Supporting
{
    public class SupportingTests : HeaderSetupFixture
    {
        public SupportingTests() : base(CommonConstants.Host.SupportingService) { }

        [TestCaseSource(typeof(ClientInfoData), nameof(ClientInfoData.CurrentSettings))]
        public void SupportingTests_GetClientInfo(string amacv, string imacv, string iimm)
        {
            Init(Constants.Path.ClientInfo, Method.GET);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(amacv, json["records"]["androidMinimumAllowedClientVersion"].ToString());
            StringAssert.AreEqualIgnoringCase(imacv, json["records"]["iosMinimumAllowedClientVersion"].ToString());
            StringAssert.AreEqualIgnoringCase(iimm, json["records"]["isInMaintenanceMode"].ToString());
            StringAssert.Contains("id", response.Content);
        }

        [TestCaseSource(typeof(ClientInfoData), nameof(ClientInfoData.CorrectSettings))]
        public void SupportingTests_AddClientInfo_Should_Change(string amacv, string imacv, string iimm)
        {
            Init(Constants.Path.ClientInfo, Method.PUT);
            request.AddJsonBody(new AddClientInfoRequest(amacv, imacv, iimm));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCaseSource(typeof(ClientInfoData), nameof(ClientInfoData.IncorrectSettings))]
        public void SupportingTests_AddClientInfo_Incorrect_Format(string amacv, string imacv, string iimm)
        {
            Init(Constants.Path.ClientInfo, Method.PUT);
            request.AddJsonBody(new AddClientInfoRequest(amacv, imacv, iimm));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("must be valid version number", json["errors"]["iosMinimumAllowedClientVersion"].First.ToString());
            StringAssert.Contains("must be valid version number", json["errors"]["androidMinimumAllowedClientVersion"].First.ToString());
        }
    }
}
