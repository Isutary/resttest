using CommonConstats = RESTTest.Common.Constants;

namespace RESTTest.Player.TestData
{
    public class SettingsData
    {
        public static object[] IncorrectColor = {
            new string[] { 
                CommonConstats.Data.Empty, 
                CommonConstats.Data.True, 
                CommonConstats.Data.False, 
                CommonConstats.Data.True 
            },
            new string[] { 
                Constants.Data.Settings.Incorrect,
                CommonConstats.Data.True,
                CommonConstats.Data.False,
                CommonConstats.Data.True
            }
        };

        public static object[] CorrectColor = {
            new string[] { 
                Constants.Data.Settings.White,
                CommonConstats.Data.True,
                CommonConstats.Data.False,
                CommonConstats.Data.True
            },
            new string[] { 
                Constants.Data.Settings.Yellow,
                CommonConstats.Data.False,
                CommonConstats.Data.False,
                CommonConstats.Data.False
            },
        };
    }
}
