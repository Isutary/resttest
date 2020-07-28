namespace RESTTest.Game
{
    public static class Constants
    {
        public static class Path
        {
            public const string Game = Common.Constants.Version + "/game";
            public const string NextGame = Game + "/nextgameinfo";
        }

        public static class Data
        {
            public static class Game
            {
                public const string Prize = "Rest auto test";
                public const string NonRecurring = "N/A";
                public const string IncorrectCode = "AAABBB";
                public const string TakenCode = "E83Q0B";
                public const string IncorrectRecurring = "a";
            }
        }
    }
}
