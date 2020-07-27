namespace RESTTest.Identity.TestData
{
    public class PasswordData
    {
        public static object[] IncorrectPassword = {
            new string[] { "asd", "Plavi.12.", "Plavi.12." }
        };

        public static object[] IncorrectPasswordLength = {
            new string[] { "Plavi.12.", "asd", "asd" }
        };

        public static object[] IncorrectRepeatPassword = {
            new string[] { "Plavi.12.", "Plavi.12.", "asd" }
        };

        public static object[] EmptyPassword = {
            new string[] { "", "", "" }
        };

        public static object[] CorrectPassword = {
            new string[] { "Plavi.12.", "Plavi.12.", "Plavi.12." }
        };
    }
}
