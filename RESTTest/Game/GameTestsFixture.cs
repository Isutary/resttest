using NUnit.Framework;
using RESTTest.Common.Generators;
using RESTTest.Common.Setup;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Game
{
    [SetUpFixture]
    public class GameTestsFixture : HeaderSetupFixture
    {
        public GameTestsFixture() : base(CommonConstants.Host.GameService) { }

        [OneTimeSetUp]
        public void SetUp()
        {
            GameGenerator.CreateGame(Constants.Data.Game.TestGameCode);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            GameGenerator.DeleteAll();
        }
    }
}
