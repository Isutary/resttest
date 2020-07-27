namespace RESTTest.Prize.TestData
{
    public class ClaimData
    {
        public static object[] AlreadyClaimed = {
            new string[] { "6351e7d8-8714-44ba-58c4-08d8272f665d", "Salt", "salt@automation.com", "+3874568711" }
        };

        public static object[] EmptyClaim = {
            new string[] { "91659763-da76-45da-29d9-08d82fe6b8d8", "", "", "" }
        };

        public static object[] CorrectClaim = {
            new string[] { "91659763-da76-45da-29d9-08d82fe6b8d8", "Salt", "salt@paypal.com", "+123456" }
        };

        public static object[] IncorrectClaim = {
            new string[] { "91659763-0000-0000-0000-08d82fe6b8d8", "Salt", "salt@paypal.com", "+123456" }
        };
    }
}
