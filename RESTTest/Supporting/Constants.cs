using System.ComponentModel;

namespace RESTTest.Supporting
{
    public static class Constants
    {
        public static class Path
        {
            public const string ClientInfo = Common.Constants.Version + "/ClientInfo";
        }

        public static class Data
        {
            public static class ClientInfo
            {
                public const string CurrentAMACV = "1.0.0";
                public const string CurrentIMACV = "0.40.35";
                public const string CurrentIIMM = "false";
                public const string Correct = "9.9.9";
                public const string Incorrect = "0";
            }
        }
    }
}
