using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Generators;
using RESTTest.Common.Setup;
using RESTTest.Registration.Requests;
using RESTTest.Registration.TestData;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;
using CommonName = RESTTest.Common.Constants.Name;
using CommonResponse = RESTTest.Common.Constants.Response;

namespace RESTTest.Registration
{
    public class RegistrationTests : HeaderSetupFixture
    {
        public RegistrationTests() : base(CommonConstants.Host.IdentityService) { }

        [TestCaseSource(typeof(DefaultImageData), nameof(DefaultImageData.CorrectDefault))]
        public void RegistrationTests_Correct_Default_Profile_Picture(string value)
        {
            Init(Constants.Path.DefaultProfilePicture + value, Method.PUT);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCaseSource(typeof(DefaultImageData), nameof(DefaultImageData.IncorrectDefault))]
        public void RegistrationTests_Incorrect_Default_Profile_Picture(string value)
        {
            Init(Constants.Path.DefaultProfilePicture + value, Method.PUT);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotExist, json[CommonName.Message].ToString());
        }

        [TestCaseSource(typeof(PlayerAccountSearchData), nameof(PlayerAccountSearchData.IncorrectAccount))]
        public void RegistrationTests_Account_Does_Not_Exist(string email)
        {
            Init(Constants.Path.Search, Method.GET);
            request.AddParameter(Constants.Query.Email, email);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(CommonResponse.False, json[CommonName.Records][Constants.Name.IsExisting].ToString());
        }

        [TestCaseSource(typeof(PlayerAccountSearchData), nameof(PlayerAccountSearchData.CorrectAccount))]
        public void RegistrationTests_Account_Does_Exist(string email)
        {
            Init(Constants.Path.Search, Method.GET);
            request.AddParameter(Constants.Query.Email, email);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(CommonResponse.True, json[CommonName.Records][Constants.Name.IsExisting].ToString());
        }

        [TestCaseSource(typeof(UsernameSearchData), nameof(UsernameSearchData.IncorrectUsername))]
        public void RegistrationTests_Username_Does_Not_Exist(string user)
        {
            Init(Constants.Path.Search, Method.GET);
            request.AddParameter(Constants.Query.Username, user);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(CommonResponse.False, json[CommonName.Records][Constants.Name.IsExisting].ToString());
        }

        [TestCaseSource(typeof(UsernameSearchData), nameof(UsernameSearchData.CorrectUsername))]
        public void RegistrationTests_Username_Does_Exist(string user)
        {
            Init(Constants.Path.Search, Method.GET);
            request.AddParameter(Constants.Query.Username, user);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(CommonResponse.True, json[CommonName.Records][Constants.Name.IsExisting].ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.EmptyInformation))]
        public void RegistrationTests_Cannot_Be_Empty(string password, string user, string email, string hand) 
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.Email].First.ToString());
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.Password].First.ToString());
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.Username].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.NotLongEnough))]
        public void RegistrationTests_Not_Long_Enough(string password, string user, string email, string hand)
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.Length, json[CommonName.Errors][Constants.Name.Password].First.ToString());
            StringAssert.Contains(Constants.Response.Between, json[CommonName.Errors][Constants.Name.Username].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.IncorrectEmail))]
        public void RegistrationTests_Incorrect_Email_Format(string password, string user, string email, string hand)
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(Constants.Response.EmailNotValid, json[CommonName.Errors][Constants.Name.Email].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.TakenUsername))]
        public void RegistrationTests_Username_Taken(string password, string user, string email, string hand)
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.IsTaken, json[CommonName.Errors].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.TakenEmail))]
        public void RegistrationTests_Email_Taken(string password, string user, string email, string hand)
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.IsTaken, json[CommonName.Errors].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.CorrectInformation))]
        public void RegistrationTests_Should_Register(string password, string user, string email, string hand)
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.Contains(CommonName.Records, json.ToString());
            PlayerGenerator.DeletePlayer(json[CommonName.Records].ToString());
        }

        [TestCase(Constants.Data.RegisterAccount.CorrectUsername, Constants.Data.RegisterAccount.CorrectEmail)]
        public void RegistrationTests_Should_Delete(string user, string email)
        {
            string id = PlayerGenerator.CreatePlayer(user, email, false);

            Init(Constants.Path.User + $"/{id}", Method.DELETE);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.WrongId))]
        public void RegistrationTests_Delete_Wrong_Id(string id)
        {
            Init(Constants.Path.User + $"/{id}", Method.DELETE);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            StringAssert.Contains(Constants.Response.UserNotFound, json[CommonName.Errors].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.IncorrectId))]
        public void RegistrationTests_Delete_Incorrect_Id(string id)
        {
            Init(Constants.Path.User + $"/{id}", Method.DELETE);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotValid, json[CommonName.Errors][Constants.Name.UserId].First.ToString());
        }
    }
}
