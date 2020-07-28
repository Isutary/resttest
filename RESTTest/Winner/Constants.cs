﻿using System;

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
    }
}
