namespace RESTTest.Registration.Requests
{
    public class RegisterAccountRequest
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HandColor { get; set; }

        public RegisterAccountRequest(string password, string username, string email, string handColor)
        {
            Password = password;
            Username = username;
            Email = email;
            HandColor = handColor;
        }
    }
}
