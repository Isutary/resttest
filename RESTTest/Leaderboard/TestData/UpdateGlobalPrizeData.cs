using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Leaderboard.TestData
{
    public class UpdateGlobalPrizeData
    {
        public static object[] EmptyPrize = {
            new string[] {
                Constants.Data.GlobalPrize.IncorrectPrizeId,
                CommonConstants.Data.Empty,
                Constants.Data.GlobalPrize.IncorrectPrizeId,
                CommonConstants.Data.Empty
            }
        };

        public static object[] CorrectPrize = {
            new string[] {
                Constants.Data.GlobalPrize.Prize,
                Constants.Data.GlobalPrize.Prize
            }
        };
    }
}
