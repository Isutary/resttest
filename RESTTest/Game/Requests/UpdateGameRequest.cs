namespace RESTTest.Game.Requests
{
    public class UpdateGameRequest
    {
        public string FirstPrize { get; set; }
        public string ConsolationPrize { get; set; }
        public string LobbyOpensAt { get; set; }
        public string EndAt { get; set; }
        public string IsRecurring { get; set; }
        public string RecurringPattern { get; set; }

        public UpdateGameRequest(
                string firstPrize,
                string consolationPrize,
                string lobbyOpensAt,
                string endAt,
                string isRecurring,
                string recurringPatter
            )
        {
            FirstPrize = firstPrize;
            ConsolationPrize = consolationPrize;
            LobbyOpensAt = lobbyOpensAt;
            EndAt = endAt;
            IsRecurring = isRecurring;
            RecurringPattern = recurringPatter;
        }
    }
}
