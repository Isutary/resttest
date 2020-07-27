namespace RESTTest.Player.TestData
{
    public class SettingsData
    {
        public static object[] IncorrectColor = {
            new string[] { "", "true", "false", "true" },
            new string[] { "red", "true", "false", "true" }
        };

        public static object[] CorrectColor = {
            new string[] { "White", "true", "false", "true" },
            new string[] { "Yellow", "false", "false", "false" },
        };
    }
}
