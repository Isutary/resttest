using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Registration.TestData
{
    public class RegisterAccountData
    {
        public static object[] EmptyInformation = { 
            new string[] { 
                CommonConstants.Data.Empty,
                CommonConstants.Data.Empty,
                CommonConstants.Data.Empty,
                Constants.Data.RegisterAccount.Hand
            }
        };

        public static object[] NotLongEnough = {
            new string[] { 
                Constants.Data.RegisterAccount.Short,
                Constants.Data.RegisterAccount.Short,
                Constants.Data.RegisterAccount.CorrectEmail,
                Constants.Data.RegisterAccount.Hand
            }
        };

        public static object[] IncorrectEmail = {
            new string[] { 
                Constants.Data.RegisterAccount.CorrectPassword,
                Constants.Data.RegisterAccount.CorrectUsername,
                Constants.Data.RegisterAccount.IncorrectEmail,
                Constants.Data.RegisterAccount.Hand
            }
        };

        public static object[] TakenUsername = {
            new string[] { 
                Constants.Data.RegisterAccount.CorrectPassword,
                Constants.Data.RegisterAccount.TakenUsername,
                Constants.Data.RegisterAccount.CorrectEmail,
                Constants.Data.RegisterAccount.Hand
            }
        };

        public static object[] TakenEmail = {
            new string[] { 
                Constants.Data.RegisterAccount.CorrectPassword,
                Constants.Data.RegisterAccount.CorrectUsername,
                Constants.Data.RegisterAccount.TakenEmail,
                Constants.Data.RegisterAccount.Hand
            }
        };

        public static object[] CorrectInformation = {
            new string[] { 
                Constants.Data.RegisterAccount.CorrectPassword,
                Constants.Data.RegisterAccount.CorrectUsername,
                Constants.Data.RegisterAccount.CorrectEmail,
                Constants.Data.RegisterAccount.Hand
            }
        };
    }
}
