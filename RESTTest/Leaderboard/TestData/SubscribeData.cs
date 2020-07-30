using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Leaderboard.TestData
{
    public class SubscribeData
    {
        public static string[] EmtpyPin = {
            CommonConstants.Data.Empty
        };

        public static string[] IncorrectPin = {
            Constants.Data.Subscribe.IncorrectPin
        };

        public static string[] AlreadySubscribed = { 
            Constants.Data.Subscribe.TestLeaderboardPin
        };
    }
}
