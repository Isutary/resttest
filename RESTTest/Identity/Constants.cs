namespace RESTTest.Identity
{
    public static class Constants
    {
        public static class Path
        {
            public const string User = Common.Constants.Version + "/user";
            public const string Email = User + "/email";
            public const string Password = User + "/password";
        }

        public static class Data
        {
            public static class Password
            {
                public const string Correct = "Plavi.12.";
                public const string Incorrect = "aaa";
            }
        }

        public static class Name
        {
            public const string Email = "email";
            public const string Failure = "IsFailure";
            public const string Success = "IsSuccess";
            public const string Error = "Error";
            public const string NewPassword = "newPassword";
            public const string RepeatPassword = "repeatPassword";
            public const string CurrentPassword = "currentPassword";
        }

        public static class Response
        {
            public const string IncorrectFormat = "is not in the correct format";
            public const string IncorrectPassword = "Incorrect password.";
            public const string MustEqual = "must be equal to";
        }
    }
}
