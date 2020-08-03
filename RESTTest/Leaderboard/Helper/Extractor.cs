using Newtonsoft.Json.Linq;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Leaderboard.Requests;
using CommonConstants = RESTTest.Common.Constants;
using CommonName = RESTTest.Common.Constants.Name;
using SetupName = RESTTest.Common.Constants.Setup.Name;

namespace RESTTest.Leaderboard.Helper
{
    public class Extractor
    {
        private readonly static RestClient client = new RestClient(CommonConstants.Host.LeaderboardService);
        
        public static string CurrentPrizeId { get; private set; }

        public static string CurrentPrize { get; private set; }
        
        public static string FuturePrizeId { get; private set; }

        public static string FuturePrize { get; private set; }

        static Extractor()
        {
            RestRequest request = new RestRequest(Constants.Path.GlobalPrize, Method.GET);
            request.AddHeader(SetupName.X_tenant_id, CommonConstants.Setup.X_tenant_id);
            request.AddHeader(SetupName.Authorization, AuthenticationSetupFixture.token);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            CurrentPrizeId = json[CommonName.Records][CommonName.CurrentPrize][CommonName.Id].ToString();
            CurrentPrize = json[CommonName.Records][CommonName.CurrentPrize][CommonName.Description].ToString();
            FuturePrizeId = json[CommonName.Records][CommonName.FuturePrize][CommonName.Id].ToString();
            FuturePrize = json[CommonName.Records][CommonName.FuturePrize][CommonName.Description].ToString();
        }

        private static void Refresh()
        {
            RestRequest request = new RestRequest(Constants.Path.GlobalPrize, Method.GET);
            request.AddHeader(SetupName.X_tenant_id, CommonConstants.Setup.X_tenant_id);
            request.AddHeader(SetupName.Authorization, AuthenticationSetupFixture.token);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            CurrentPrizeId = json[CommonName.Records][CommonName.CurrentPrize][CommonName.Id].ToString();
            FuturePrizeId = json[CommonName.Records][CommonName.FuturePrize][CommonName.Id].ToString();
        }

        public static void Revert()
        {
            Refresh();

            RestRequest request = new RestRequest(Constants.Path.GlobalPrize, Method.GET);
            request.AddHeader(SetupName.X_tenant_id, CommonConstants.Setup.X_tenant_id);
            request.AddHeader(SetupName.Authorization, AuthenticationSetupFixture.token);

            request.AddJsonBody(new UpdateGlobalPrizeRequest(CurrentPrizeId, CurrentPrize, FuturePrizeId, FuturePrize));

            client.Execute(request);
        }
    }
}
