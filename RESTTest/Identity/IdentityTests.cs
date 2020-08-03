using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Identity.Requests;
using RESTTest.Identity.TestData;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;
using CommonName = RESTTest.Common.Constants.Name;
using CommonResponse = RESTTest.Common.Constants.Response;

namespace RESTTest.Identity
{
    public class IdentityTests : HeaderSetupFixture
    {
        public IdentityTests() : base(CommonConstants.Host.IdentityService) { }

        [TestCaseSource(typeof(EmailData), nameof(EmailData.TakenEmail))]
        public void IdentityTests_Email_Is_Taken(string email)
        {
            Init(Constants.Path.Email, Method.PUT);
            request.AddJsonBody(new UpdateEmailRequest(email));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.IsTaken, json[CommonName.Errors].First.ToString());
        }

        [TestCaseSource(typeof(EmailData), nameof(EmailData.IncorrectEmail))]
        public void IdentityTests_Email_Format_Is_Incorrect(string email)
        {
            Init(Constants.Path.Email, Method.PUT);
            request.AddJsonBody(new UpdateEmailRequest(email));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(Constants.Response.IncorrectFormat, json[CommonName.Errors][Constants.Name.Email].First.ToString());
        }

        [TestCaseSource(typeof(EmailData), nameof(EmailData.CorrectEmail))]
        public void IdentityTests_Email_Should_Change(string email)
        {
            Init(Constants.Path.Email, Method.PUT);
            request.AddJsonBody(new UpdateEmailRequest(email));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCaseSource(typeof(PasswordData), nameof(PasswordData.IncorrectPassword))]
        public void IdentityTests_Password_Incorrect_Password(string currentPassword, string newPassword, string repeatPassword)
        {
            Init(Constants.Path.Password, Method.PUT);
            request.AddJsonBody(new UpdatePasswordRequest(currentPassword, newPassword, repeatPassword));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(CommonResponse.True, json[Constants.Name.Failure].ToString());
            StringAssert.AreEqualIgnoringCase(CommonResponse.False, json[Constants.Name.Success].ToString());
            StringAssert.AreEqualIgnoringCase(Constants.Response.IncorrectPassword, json[Constants.Name.Error].ToString());
        }

        [TestCaseSource(typeof(PasswordData), nameof(PasswordData.IncorrectPasswordLength))]
        public void IdentityTests_Password_Not_Long_Enough(string currentPassword, string newPassword, string repeatPassword)
        {
            Init(Constants.Path.Password, Method.PUT);
            request.AddJsonBody(new UpdatePasswordRequest(currentPassword, newPassword, repeatPassword));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.Length, json[CommonName.Errors][Constants.Name.NewPassword].First.ToString());
        }

        [TestCaseSource(typeof(PasswordData), nameof(PasswordData.IncorrectRepeatPassword))]
        public void IdentityTests_Password_Must_Match(string currentPassword, string newPassword, string repeatPassword)
        {
            Init(Constants.Path.Password, Method.PUT);
            request.AddJsonBody(new UpdatePasswordRequest(currentPassword, newPassword, repeatPassword));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(Constants.Response.MustEqual, json[CommonName.Errors][Constants.Name.RepeatPassword].First.ToString());
        }

        [TestCaseSource(typeof(PasswordData), nameof(PasswordData.EmptyPassword))]
        public void IdentityTests_Password_Cannot_Be_Empty(string currentPassword, string newPassword, string repeatPassword)
        {
            Init(Constants.Path.Password, Method.PUT);
            request.AddJsonBody(new UpdatePasswordRequest(currentPassword, newPassword, repeatPassword));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.NewPassword].First.ToString());
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.RepeatPassword].First.ToString());
            StringAssert.Contains(CommonResponse.NotEmpty, json[CommonName.Errors][Constants.Name.CurrentPassword].First.ToString());
        }

        [TestCaseSource(typeof(PasswordData), nameof(PasswordData.CorrectPassword))]
        public void IdentityTests_Password_Should_Change(string currentPassword, string newPassword, string repeatPassword)
        {
            Init(Constants.Path.Password, Method.PUT);
            request.AddJsonBody(new UpdatePasswordRequest(currentPassword, newPassword, repeatPassword));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase(CommonResponse.False, json[Constants.Name.Failure].ToString());
            StringAssert.AreEqualIgnoringCase(CommonResponse.True, json[Constants.Name.Success].ToString());
        }
    }
}
