namespace RESTTest.Supporting.TestData
{
    public class ClientInfoData
    {
        public static object[] CurrentSettings = { 
            new string[] { "1.0.0", "0.40.35", "false" }
        };

        public static object[] CorrectSettings = {
            new string[] { "2.0.0", "1.40.35", "true" },
            CurrentSettings[0]
        };

        public static object[] IncorrectSettings = { 
            new string[] { "0", "0", "false" }
        };
    }
}
