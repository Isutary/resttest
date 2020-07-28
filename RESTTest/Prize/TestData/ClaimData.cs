using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Prize.TestData
{
    public class ClaimData
    {
        public static object[] AlreadyClaimed = {
            new string[] { 
                Constants.Data.Claim.TakenId,
                Constants.Data.Claim.Username,
                Constants.Data.Claim.Email,
                Constants.Data.Claim.Phone
            }
        };

        public static object[] EmptyClaim = {
            new string[] { 
                Constants.Data.Claim.CorrectId,
                CommonConstants.Data.Empty,
                CommonConstants.Data.Empty,
                CommonConstants.Data.Empty
            }
        };

        public static object[] CorrectClaim = {
            new string[] { 
                Constants.Data.Claim.CorrectId,
                Constants.Data.Claim.Username,
                Constants.Data.Claim.Email,
                Constants.Data.Claim.Phone
            }
        };

        public static object[] IncorrectClaim = {
            new string[] {
                Constants.Data.Claim.IncorrectId,
                Constants.Data.Claim.Username,
                Constants.Data.Claim.Email,
                Constants.Data.Claim.Phone
            }
        };
    }
}
