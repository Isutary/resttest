namespace RESTTest.Identity.Requests
{
    public class UpdatePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }

        public UpdatePasswordRequest(string currentPassword, string newPassword, string repeatPassword)
        {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            RepeatPassword = repeatPassword;
        }
    }
}
