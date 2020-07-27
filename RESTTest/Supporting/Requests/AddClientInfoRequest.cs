namespace RESTTest.Supporting.Requests
{
    public class AddClientInfoRequest
    {
        public string AndroidMinimumAllowedClientVersion;

        public string IosMinimumAllowedClientVersion;

        public string IsInMaintenanceMode;

        public AddClientInfoRequest(string amacv, string imacv, string iimm)
        {
            AndroidMinimumAllowedClientVersion = amacv;
            IosMinimumAllowedClientVersion = imacv;
            IsInMaintenanceMode = iimm;
        }
    }
}
