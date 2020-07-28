using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Supporting.TestData
{
    public class ClientInfoData
    {
        public static object[] CurrentSettings = { 
            new string[] { 
                Constants.Data.ClientInfo.CurrentAMACV, 
                Constants.Data.ClientInfo.CurrentIMACV, 
                Constants.Data.ClientInfo.CurrentIIMM 
            }
        };

        public static object[] CorrectSettings = {
            new string[] {
                Constants.Data.ClientInfo.Correct,
                Constants.Data.ClientInfo.Correct,
                CommonConstants.Data.True
            },
            CurrentSettings[0]
        };

        public static object[] IncorrectSettings = { 
            new string[] {
                Constants.Data.ClientInfo.Incorrect,
                Constants.Data.ClientInfo.Incorrect,
                CommonConstants.Data.False
            }
        };
    }
}
