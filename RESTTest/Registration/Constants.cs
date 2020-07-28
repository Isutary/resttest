namespace RESTTest.Registration
{
    public static class Constants
    {
        public static class Path
        {
            public const string PlayerID = "/941acd9e-4006-494c-9dbb-0a3dbd3d6050";
            public const string User = Common.Constants.Version + "/User";
            public const string Register = User + "/register";
            public const string Search = Register + "/search";
            public const string ProfilePicture = Register + PlayerID + "/profile-image";
            public const string DefaultProfilePicture = User + "/profile-image/default/DefaultImage";
        }

        public static class Data
        {
            public static class DefaultImage
            {
                public const string Correct = "1";
                public const string Incorrect = "10";
            }

            public static class AccountSearch
            {
                public const string Correct = "salt@test.com";
                public const string Incorrect = "randomaccountname@random.com";
            }

            public static class RegisterAccount
            {
                public const string Hand = "Black";
                public const string Short = "a";
                public const string CorrectEmail = "megarandomemail@test.com";
                public const string IncorrectEmail = "salttest.com";
                public const string TakenEmail = "salt@test.com";
                public const string CorrectPassword = "Plavi.12.";
                public const string CorrectUsername = "AlsoAmazinglyRandomName";
                public const string TakenUsername = "Salt";
            }

            public static class UsernameSearch
            {
                public const string Incorrect = "SoRandomImAmazed";
                public const string Correct = "salt";
            }
        }
    }
}
