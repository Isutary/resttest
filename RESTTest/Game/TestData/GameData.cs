using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Game.TestData
{
    public class GameData
    {
        public static object[] IncorrectGameCode = {
            new string[] {
                CommonConstants.Data.Empty,
                Constants.Data.Game.Prize,
                Constants.Data.Game.Prize,
                CommonConstants.Data.False,
                Constants.Data.Game.NonRecurring
            }
        };

        public static object[] EmptyFirstPrize = {
            new string[] {
                Constants.Data.Game.IncorrectCode,
                CommonConstants.Data.Empty,
                Constants.Data.Game.Prize,
                CommonConstants.Data.False,
                Constants.Data.Game.NonRecurring
            }
        };

        public static object[] EmptyConsolationPrize = {
            new string[] {
                Constants.Data.Game.IncorrectCode,
                Constants.Data.Game.Prize,
                CommonConstants.Data.Empty,
                CommonConstants.Data.False,
                Constants.Data.Game.NonRecurring
            }
        };

        public static object[] CorrectInformation = {
            new string[] {
                Constants.Data.Game.IncorrectCode,
                Constants.Data.Game.Prize,
                Constants.Data.Game.Prize,
                CommonConstants.Data.False,
                Constants.Data.Game.NonRecurring
            }
        };

        public static object[] IncorrectRecurring = {
            new string[] {
                Constants.Data.Game.IncorrectCode,
                Constants.Data.Game.Prize,
                Constants.Data.Game.Prize,
                CommonConstants.Data.True,
                Constants.Data.Game.IncorrectRecurring
            }
        };

        public static object[] TakenGameCode = {
            new string[] {
                Constants.Data.Game.TakenCode,
                Constants.Data.Game.Prize,
                Constants.Data.Game.Prize,
                CommonConstants.Data.False,
                Constants.Data.Game.NonRecurring
            }
        };
    }
}
