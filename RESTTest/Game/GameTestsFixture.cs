using NUnit.Framework;
using RestSharp;
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
            Init(Constants.Path.Game, Method.POST);
            GameGenerator.CreateGame(client, request, Constants.Data.Game.TestGameCode);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Init(Constants.Path.Game + $"/{GameGenerator.Id}", Method.DELETE);
            GameGenerator.DeleteGame(client, request);
        }
    }
}
