using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Leaderboard
{
    public static class Constants
    {
        public static class Path
        {
            public const string Id = "/24492969-e601-4bfc-1970-08d8347de80c";
            public const string This = "/this-week";
            public const string Previous = "/previous-week";
            public const string Leaderboard = CommonConstants.Version + "/Leaderboard";
            public const string GlobalPrize = Leaderboard + "/global/prize";
            public const string Unsubscribe = Leaderboard + "/unsubscribe";
            public const string Subscribe = Leaderboard + "/subscribe";
            public const string MyLeaderboards = Leaderboard + "/my/leaderboards";
            public const string GlobalThis = Leaderboard + "/global" + This;
            public const string GlobalPrevious = Leaderboard + "/global" + Previous;
            public const string GlobalPositionThis = Leaderboard + "/my/global" + This + "/position";
            public const string GlobalPositionPrevious = Leaderboard + "/my/global" + Previous + "/position";
        }

        public static class Data
        {
            public static class Leaderboard
            {
                public const string LongName = "aaaaaaaaaaaaaaaaaaaa";
                public const string CorrectName = "Temp";
                public const string TestLeaderboardName = "LeaderboardTest";
            }

            public static class Subscribe
            {
                public const string IncorrectPin = "AAAAAA";
                public const string TestLeaderboardPin = "L3E95C";
            }

            public static class GlobalPrize
            {
                public const string IncorrectPrizeId = "2CC641A7-1057-4E28-82AD-D5C4C8F06164";
                public const string Prize = "Rest test prize";
            }
        }

        public static class Name
        {
            public const string LeaderboardName = "name";
            public const string Type = "type";
            public const string CreatedById = "createdById";
        }

        public static class Response
        {
            public const string Type = "Custom";
            public const string CreatedById = "2d84c01d-5885-4c51-91a0-045ef918d429";
            public const string TooLong = "must be 18 characters or fewer";
            public const string AlreadySubscribed = "already subscribed to leaderboard";
        }
    }
}
