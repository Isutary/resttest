namespace RESTTest.Game
{
    public static class Constants
    {
        public static class Path
        {
            public const string Game = Common.Constants.Version + "/game";
            public const string NextGame = Game + "/nextgameinfo";
        }

        public static class Query
        {
            public const string Page = "1";
            public const string PageSize = "50";
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
                public const string CorrectCode = "999999";
                public const string TestGameCode = "000000";
            }
        }

        public static class Name
        {
            public const string First = "firstPrize";
            public const string Consolation = "consolationPrize";
            public const string EndAt = "endAt";
            public const string Code = "gameCode";
            public const string NextGameDate = "nextGameDate";
            public const string NextGameId = "nextGameId";
            public const string NextGamePrizeDescription = "nextGamePrizeDescription";
            public const string NextGameConsolationPrizeDescription = "nextGameConsolationPrizeDescription";
            public const string PreviousWinner = "previousWinner";
            public const string PreviousGamePrizeDescription = "previousGamePrizeDescription";
            public const string PreviousGameConsolationPrizeDescription = "previousGameConsolationPrizeDescription";
        }

        public static class Response
        {
            public const string GreaterThan = "must be greater than";
            public const string MustBeSame = "StartAt and EndAt must be the same";
            public const string Exists = "already exits";
        }
    }
}
