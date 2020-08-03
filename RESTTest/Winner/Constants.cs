using System;

namespace RESTTest.Winner
{
    public static class Constants
    {
        public static class Path
        {
            public const string PrizeId = "/91659763-da76-45da-29d9-08d82fe6b8d8";
            public const string Winner = Common.Constants.Version + "/winner";
            public const string Prize = Winner + PrizeId;
        }

        public static class Query
        {
            public const string Page = "1";
            public const string PageSize = "10";
        }

        public static class Data
        {
            public static class Claim
            {
                public const string Claimed = "Claimed";
                public const string Pending = "Pending";
                public const string NotClaimed = "Not claimed";
            }
        }

        public static class Response
        {
            public const string Username = "Salt";
            public const string Email = "salt@automation.com";
            public const string Phone = "+381257";
            public const string TakenId = "6351e7d8-8714-44ba-58c4-08d8272f665d";
            public const string CorrectId = "91659763-da76-45da-29d9-08d82fe6b8d8";
            public const string IncorrectId = "91659763-0000-0000-0000-08d82fe6b8d8";
            public const string Position = "1st";
            public const string Status = "Not claimed";
            public const string Prize = "RESTTest Prize";
            public const string Type = "Game prize";
            public const string Page = Query.Page;
            public const string PageSize = Query.PageSize;
        }
    }
}
