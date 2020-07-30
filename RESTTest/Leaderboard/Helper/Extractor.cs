using Newtonsoft.Json.Linq;
using RestSharp;
using RESTTest.Common.Setup;
using RESTTest.Leaderboard.Requests;
using CommonConstants = RESTTest.Common.Constants;

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
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            CurrentPrizeId = json["records"]["currentPrize"]["id"].ToString();
            CurrentPrize = json["records"]["currentPrize"]["prizeDescription"].ToString();
            FuturePrizeId = json["records"]["futurePrize"]["id"].ToString();
            FuturePrize = json["records"]["futurePrize"]["prizeDescription"].ToString();
        }

        private static void Refresh()
        {
            RestRequest request = new RestRequest(Constants.Path.GlobalPrize, Method.GET);
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);

            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            CurrentPrizeId = json["records"]["currentPrize"]["id"].ToString();
            FuturePrizeId = json["records"]["futurePrize"]["id"].ToString();
        }

        public static void Revert()
        {
            Refresh();

            RestRequest request = new RestRequest(Constants.Path.GlobalPrize, Method.GET);
            request.AddHeader("x-tenant-id", CommonConstants.Setup.X_tenant_id);
            request.AddHeader("Authorization", AuthenticationSetupFixture.token);

            request.AddJsonBody(new UpdateGlobalPrizeRequest(CurrentPrizeId, CurrentPrize, FuturePrizeId, FuturePrize));

            client.Execute(request);
        }
    }
}
