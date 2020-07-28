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
        }
    }
}
