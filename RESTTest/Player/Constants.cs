namespace RESTTest.Player
{
    public static class Constants
    {
        public static class Path
        {
            public const string PlayerID = "/2d84c01d-5885-4c51-91a0-045ef918d429";
            public const string Player = Common.Constants.Version + "/Player";
            public const string GoldenLives = Player + PlayerID + "/golden-lives";
            public const string Settings = Player + "/settings";
            public const string FriendCode = Player + "/friend-code";
        }

        public static class Data
        {
            public static class FriendCode
            {
                public const string Incorrect = "somerandomnamenoway";
                public const string Correct = "pepper";
            }

            public static class GoldenLives
            {
                public const string Negative = "-1";
            }

            public static class Settings
            {
                public const string Incorrect = "red";
                public const string Yellow = "Yellow";
                public const string White = "White";
            }
        }
    }
}
