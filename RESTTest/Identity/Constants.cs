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
            public static class Email
            {

            }

            public static class Password
            {
                public const string Correct = "Plavi.12.";
                public const string Incorrect = "aaa";
            }
        }
    }
}
