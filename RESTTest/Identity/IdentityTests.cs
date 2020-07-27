using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RESTTest.Identity.Requests;
using RESTTest.Identity.TestData;
using System.Net;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Identity
{
    public class IdentityTests : HeaderSetupFixture
    {
        public IdentityTests() : base(CommonConstants.RestClient.IdentityService) { }

        [TestCaseSource(typeof(EmailData), nameof(EmailData.TakenEmail))]
        public void IdentityTests_Email_Is_Taken(string email)
        {
            Init(Constants.Email, Method.PUT);
            request.AddJsonBody(new UpdateEmailRequest(email));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("is already taken", json["errors"].First.ToString());
        }

        [TestCaseSource(typeof(EmailData), nameof(EmailData.IncorrectEmail))]
        public void IdentityTests_Email_Format_Is_Incorrect(string email)
        {
            Init(Constants.Email, Method.PUT);
            request.AddJsonBody(new UpdateEmailRequest(email));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("is not in the correct format", json["errors"]["email"].First.ToString());
        }

        [TestCaseSource(typeof(EmailData), nameof(EmailData.CorrectEmail))]
        public void IdentityTests_Email_Should_Change(string email)
        {
            Init(Constants.Email, Method.PUT);
            request.AddJsonBody(new UpdateEmailRequest(email));

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCaseSource(typeof(PasswordData), nameof(PasswordData.IncorrectPassword))]
        public void IdentityTests_Password_Incorrect_Password(string currentPassword, string newPassword, string repeatPassword)
        {
            Init(Constants.Password, Method.PUT);
            request.AddJsonBody(new UpdatePasswordRequest(currentPassword, newPassword, repeatPassword));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.AreEqualIgnoringCase("true", json["IsFailure"].ToString());
            StringAssert.AreEqualIgnoringCase("false", json["IsSuccess"].ToString());
            StringAssert.AreEqualIgnoringCase("Incorrect password.", json["Error"].ToString());
        }

        [TestCaseSource(typeof(PasswordData), nameof(PasswordData.IncorrectPasswordLength))]
        public void IdentityTests_Password_Not_Long_Enough(string currentPassword, string newPassword, string repeatPassword)
        {
            Init(Constants.Password, Method.PUT);
            request.AddJsonBody(new UpdatePasswordRequest(currentPassword, newPassword, repeatPassword));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("must be at least 6 characters", json["errors"]["newPassword"].First.ToString());
        }

        [TestCaseSource(typeof(PasswordData), nameof(PasswordData.IncorrectRepeatPassword))]
        public void IdentityTests_Password_Must_Match(string currentPassword, string newPassword, string repeatPassword)
        {
            Init(Constants.Password, Method.PUT);
            request.AddJsonBody(new UpdatePasswordRequest(currentPassword, newPassword, repeatPassword));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("'Repeat Password' must be equal to", json["errors"]["repeatPassword"].First.ToString());
        }

        [TestCaseSource(typeof(PasswordData), nameof(PasswordData.EmptyPassword))]
        public void IdentityTests_Password_Cannot_Be_Empty(string currentPassword, string newPassword, string repeatPassword)
        {
            Init(Constants.Password, Method.PUT);
            request.AddJsonBody(new UpdatePasswordRequest(currentPassword, newPassword, repeatPassword));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            StringAssert.Contains("must not be empty", json["errors"]["newPassword"].First.ToString());
            StringAssert.Contains("must not be empty", json["errors"]["repeatPassword"].First.ToString());
            StringAssert.Contains("must not be empty", json["errors"]["currentPassword"].First.ToString());
        }

        [TestCaseSource(typeof(PasswordData), nameof(PasswordData.CorrectPassword))]
        public void IdentityTests_Password_Should_Change(string currentPassword, string newPassword, string repeatPassword)
        {
            Init(Constants.Password, Method.PUT);
            request.AddJsonBody(new UpdatePasswordRequest(currentPassword, newPassword, repeatPassword));

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            StringAssert.AreEqualIgnoringCase("false", json["IsFailure"].ToString());
            StringAssert.AreEqualIgnoringCase("true", json["IsSuccess"].ToString());
        }
    }
}
