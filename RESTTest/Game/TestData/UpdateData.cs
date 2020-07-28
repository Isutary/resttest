using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Game.TestData
{
    public class UpdateData
    {
        public static object[] EmptyFirstPrize = {
            new string[] {
                CommonConstants.Data.Empty,
                Constants.Data.Game.Prize,
                CommonConstants.Data.False,
                Constants.Data.Game.NonRecurring
            }
        };

        public static object[] EmptyConsolationPrize = {
            new string[] {
                Constants.Data.Game.Prize,
                CommonConstants.Data.Empty,
                CommonConstants.Data.False,
                Constants.Data.Game.NonRecurring
            }
        };

        public static object[] CorrectInformation = {
            new string[] {
                Constants.Data.Game.Prize,
                Constants.Data.Game.Prize,
                CommonConstants.Data.False,
                Constants.Data.Game.NonRecurring
            }
        };
    }
}
