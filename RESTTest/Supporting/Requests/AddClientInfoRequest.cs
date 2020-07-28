namespace RESTTest.Supporting.Requests
{
    public class AddClientInfoRequest
    {
        public string AndroidMinimumAllowedClientVersion { get; set; }
        public string IosMinimumAllowedClientVersion { get; set; }
        public string IsInMaintenanceMode { get; set; }

        public AddClientInfoRequest(string amacv, string imacv, string iimm)
        {
            AndroidMinimumAllowedClientVersion = amacv;
            IosMinimumAllowedClientVersion = imacv;
            IsInMaintenanceMode = iimm;
        }
    }
}
