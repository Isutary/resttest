namespace RESTTest.Leaderboard.Requests
{
    public class UpdateGlobalPrizeRequest
    {
        public string CurrentPrizeId { get; set; }
        public string CurrentPrize { get; set; }
        public string FuturePrizeId { get; set; }
        public string FuturePrize { get; set; }

        public UpdateGlobalPrizeRequest(
            string currentPrizeId,
            string currentPrize,
            string futurePrizeId,
            string futurePrize
        ) => (CurrentPrizeId, CurrentPrize, FuturePrizeId, FuturePrize) = (currentPrizeId, currentPrize, futurePrizeId, futurePrize);
    }
}
