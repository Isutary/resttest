namespace RESTTest.Common
{
    public static class Constants
    {
        public const string Version = "/api/v1.0";

        public static class Host
        {
            public const string SupportingService = "https://supporting-service.dev.rps-live.applicita.com";
            public const string IdentityService = "https://identity-service.dev.rps-live.applicita.com";
            public const string GameService = "https://game-service.dev.rps-live.applicita.com";
            public const string LeaderboardService = "https://leaderboard-service.dev.rps-live.applicita.com";
        }

        public static class Name
        {
            public const string Errors = "errors";
            public const string Records = "records";
            public const string Message = "message";
            public const string Page = Query.Page;
            public const string PageSize = Query.PageSize;
            public const string FirstName = "firstName";
            public const string Paypal = "paypalAddress";
            public const string Phone = "phoneNumber";
            public const string Username = "userName";
            public const string ClaimId = "claimRequestId";
            public const string Position = "prizePosition";
            public const string Description = "prizeDescription";
            public const string Status = "status";
            public const string Id = "id";
            public const string HostId = "hostId";
            public const string PlayerId = "playerId";
            public const string AwardedAt = "awardedAt";
            public const string Type = "prizeType";
            public const string Info = "paginationInfo";
            public const string CurrentPrize = "currentPrize";
            public const string FuturePrize = "futurePrize";
        }

        public static class Response
        {
            public const string Length = "must be at least 6 characters";
            public const string False = "false";
            public const string True = "true";
            public const string NotExist = "does not exist";
            public const string NotEmpty = "must not be empty";
            public const string IsTaken = "is already taken";
            public const string NotValid = "not valid";
            public const string NotNullOrEmpty = "cannot be null or empty";
        }

        public static class Query
        {
            public const string To = "to";
            public const string From = "from";
            public const string Page = "page";
            public const string PageSize = "pageSize";
        }

        public static class Time
        {
            public const string Format = "yyyy-MM-ddTHH:mm:ssZ";
        }

        public static class Data
        {
            public const string Empty = "";
            public const string True = "true";
            public const string False = "false";
        }

        public static class Setup
        {
            public const string X_tenant_id = "rps-live";

            public static class Authentication
            {
                public const string Client_id = "rps-live-mobile-client";
                public const string Client_secret = "39EEE5EC46AF48A6836C65EA636D1C5A";
                public const string Response_type = "code id_token";
                public const string Scope = "openid profile email aaf-notification-service-api aaf-leaderboard-service-api aaf-reporting-service-api aaf-tenant-service-api aaf-game-service-api aaf-audit-service-api aaf-identity-service-api aaf-supporting-service-api offline_access";
                public const string Grant_type = "password";
                public const string Username = "salt@test.com";
                public const string Password = "Plavi.12.";
                public const string Path = "connect/token";
            }

            public static class Name
            {
                public const string X_tenant_id = "x-tenant-id";
                public const string Authorization = "Authorization";
                public const string Access_token = "access_token";
            }
        }
    }
}
