namespace RESTTest.Registration.TestData
{
    public class RegisterAccountData
    {
        public static object[] EmptyInformation = { 
            new string[] { "", "", "", "Black" }
        };

        public static object[] NotLongEnough = {
            new string[] { "a", "a", "salt@test.com", "Black" }
        };

        public static object[] IncorrectEmail = {
            new string[] { "Plavi.12.", "Username", "salttest.com", "Black" }
        };

        public static object[] TakenUsername = {
            new string[] { "Plavi.12.", "Salt", "salt@test.com", "Black" }
        };

        public static object[] TakenEmail = {
            new string[] { "Plavi.12.", "Username", "salt@test.com", "Black" }
        };

        public static object[] CorrectInformation = {
            new string[] { "Plavi.12.", "UltraRandomUsername", "megarandomemail@test.com", "Black" }
        };
    }
}
