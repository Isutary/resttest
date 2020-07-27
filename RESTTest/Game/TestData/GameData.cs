namespace RESTTest.Game.TestData
{
    public class GameData
    {
        public static object[] EmptyGameCode = {
            new string[] {
                "",
                "Rest auto test",
                "Rest auto test",
                "false",
                "N/A"
            }
        };

        public static object[] EmptyFirstPrize = {
            new string[] {
                "AAABBB",
                "",
                "Rest auto test",
                "false",
                "N/A"
            }
        };

        public static object[] EmptyConsolationPrize = {
            new string[] {
                "AAABBB",
                "Rest auto test",
                "",
                "false",
                "N/A"
            }
        };

        public static object[] CorrectInformation = {
            new string[] {
                "AAABBB",
                "Rest auto test",
                "Rest auto test",
                "false",
                "N/A"
            }
        };

        public static object[] IncorrectRecurring = {
            new string[] {
                "AAABBB",
                "Rest auto test",
                "Rest auto test",
                "true",
                "a"
            }
        };

        public static object[] TakenGameCode = {
            new string[] {
                "E83Q0B",
                "Rest auto test",
                "Rest auto test",
                "false",
                "N/A"
            }
        };
    }
}
