using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Supporting.Requests;
using RESTTest.Supporting.TestData;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;
using CommonName = RESTTest.Common.Constants.Name;

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
            StringAssert.AreEqualIgnoringCase(amacv, json[CommonName.Records][Constants.Name.AMACV].ToString());
            StringAssert.AreEqualIgnoringCase(imacv, json[CommonName.Records][Constants.Name.IMACV].ToString());
            StringAssert.AreEqualIgnoringCase(iimm, json[CommonName.Records][Constants.Name.IIMM].ToString());
            StringAssert.Contains(Constants.Response.Id, response.Content);
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
            StringAssert.Contains(Constants.Response.MustBeValid, json[CommonName.Errors][Constants.Name.IMACV].First.ToString());
            StringAssert.Contains(Constants.Response.MustBeValid, json[CommonName.Errors][Constants.Name.AMACV].First.ToString());
        }
    }
}
