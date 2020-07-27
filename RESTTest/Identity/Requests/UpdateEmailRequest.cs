namespace RESTTest.Identity.Requests
{
    public class UpdateEmailRequest
    {
        public string Email { get; set; }

        public UpdateEmailRequest(string email)
        {
            Email = email;
        }
    }
}
