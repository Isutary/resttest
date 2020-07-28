using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Identity.TestData
{
    public class PasswordData
    {
        public static object[] IncorrectPassword = {
            new string[] { 
                Constants.Data.Password.Incorrect, 
                Constants.Data.Password.Correct, 
                Constants.Data.Password.Correct 
            }
        };

        public static object[] IncorrectPasswordLength = {
            new string[] {
                Constants.Data.Password.Correct,
                Constants.Data.Password.Incorrect, 
                Constants.Data.Password.Incorrect
            }
        };

        public static object[] IncorrectRepeatPassword = {
            new string[] { 
                Constants.Data.Password.Correct,
                Constants.Data.Password.Correct,
                Constants.Data.Password.Incorrect
            }
        };

        public static object[] EmptyPassword = {
            new string[] {
                CommonConstants.Data.Empty,
                CommonConstants.Data.Empty,
                CommonConstants.Data.Empty
            }
        };

        public static object[] CorrectPassword = {
            new string[] { 
                Constants.Data.Password.Correct,
                Constants.Data.Password.Correct,
                Constants.Data.Password.Correct
            }
        };
    }
}
