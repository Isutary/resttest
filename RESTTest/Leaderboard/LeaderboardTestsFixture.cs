using NUnit.Framework;
using RESTTest.Common.Generators;
using RESTTest.Common.Setup;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Leaderboard
{
    [SetUpFixture]
    public class LeaderboardTestsFixture : HeaderSetupFixture
    {
        public LeaderboardTestsFixture() : base(CommonConstants.Host.LeaderboardService) { }

        [OneTimeSetUp]
        public void SetUp()
        {
            LeaderboardGenerator.CreateLeaderboard(Constants.Data.Leaderboard.TestLeaderboardName);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            LeaderboardGenerator.DeleteAll();
        }
    }
}
