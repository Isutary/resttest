using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Generators;
using RESTTest.Common.Setup;
using RESTTest.Registration.Requests;
using RESTTest.Registration.TestData;
using System.Collections.Generic;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;

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
            StringAssert.Contains("does not exist and cannot be cast", json["message"].ToString());
        }

        [TestCaseSource(typeof(PlayerAccountSearchData), nameof(PlayerAccountSearchData.IncorrectAccount))]
        public void RegistrationTests_Account_Does_Not_Exist(string email)
        {
            Init(Constants.Path.Search, Method.GET);
            request.AddParameter("email", email);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase("false", json["records"]["isAlreadyExisting"].ToString());
        }

        [TestCaseSource(typeof(PlayerAccountSearchData), nameof(PlayerAccountSearchData.CorrectAccount))]
        public void RegistrationTests_Account_Does_Exist(string email)
        {
            Init(Constants.Path.Search, Method.GET);
            request.AddParameter("email", email);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase("true", json["records"]["isAlreadyExisting"].ToString());
        }

        [TestCaseSource(typeof(UsernameSearchData), nameof(UsernameSearchData.IncorrectUsername))]
        public void RegistrationTests_Username_Does_Not_Exist(string user)
        {
            Init(Constants.Path.Search, Method.GET);
            request.AddParameter("username", user);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase("false", json["records"]["isAlreadyExisting"].ToString());
        }

        [TestCaseSource(typeof(UsernameSearchData), nameof(UsernameSearchData.CorrectUsername))]
        public void RegistrationTests_Username_Does_Exist(string user)
        {
            Init(Constants.Path.Search, Method.GET);
            request.AddParameter("username", user);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase("true", json["records"]["isAlreadyExisting"].ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.EmptyInformation))]
        public void RegistrationTests_Cannot_Be_Empty(string password, string user, string email, string hand) 
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("'Email' must not be empty", json["errors"]["email"].First.ToString());
            StringAssert.Contains("'Password' must not be empty", json["errors"]["password"].First.ToString());
            StringAssert.Contains("'Username' must not be empty", json["errors"]["username"].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.NotLongEnough))]
        public void RegistrationTests_Not_Long_Enough(string password, string user, string email, string hand)
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("must be at least 6 characters", json["errors"]["password"].First.ToString());
            StringAssert.Contains("must be between 3 and 25 characters", json["errors"]["username"].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.IncorrectEmail))]
        public void RegistrationTests_Incorrect_Email_Format(string password, string user, string email, string hand)
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("'Email' is not a valid email address", json["errors"]["email"].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.TakenUsername))]
        public void RegistrationTests_Username_Taken(string password, string user, string email, string hand)
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("is already taken", json["errors"].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.TakenEmail))]
        public void RegistrationTests_Email_Taken(string password, string user, string email, string hand)
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("is already taken", json["errors"].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.CorrectInformation))]
        public void RegistrationTests_Should_Register(string password, string user, string email, string hand)
        {
            Init(Constants.Path.Register, Method.POST);
            request.AddJsonBody(new RegisterAccountRequest(password, user, email, hand));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.Contains("records", json.ToString());
            PlayerGenerator.DeletePlayer(json["records"].ToString());
        }

        [TestCase(Constants.Data.RegisterAccount.CorrectUsername, Constants.Data.RegisterAccount.CorrectEmail)]
        public void RegistrationTests_Should_Delete(string user, string email)
        {
            string id = PlayerGenerator.CreatePlayer(user, email, false);

            Init(Constants.Path.User + $"/{id}", Method.DELETE);

            IRestResponse response = client.Execute(request);
            while (response.StatusCode != HttpStatusCode.OK) response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.WrongId))]
        public void RegistrationTests_Delete_Wrong_Id(string id)
        {
            Init(Constants.Path.User + $"/{id}", Method.DELETE);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            StringAssert.Contains("user cannot be found by id", json["errors"].First.ToString());
        }

        [TestCaseSource(typeof(RegisterAccountData), nameof(RegisterAccountData.IncorrectId))]
        public void RegistrationTests_Delete_Incorrect_Id(string id)
        {
            Init(Constants.Path.User + $"/{id}", Method.DELETE);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("not valid", json["errors"]["userId"].First.ToString());
        }
    }
}
