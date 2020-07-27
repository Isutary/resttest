namespace RESTTest.Game.Requests
{
    public class AddGameRequest
    {
        public string GameCode { get; set; }
        public string FirstPrize { get; set; }
        public string ConsolationPrize { get; set; }
        public string LobbyOpensAt { get; set; }
        public string EndAt { get; set; }
        public string IsRecurring { get; set; }
        public string RecurringPattern { get; set; }

        public AddGameRequest(
                string gameCode,
                string firstPrize,
                string consolationPrize,
                string lobbyOpensAt,
                string endAt,
                string isRecurring,
                string recurringPatter
            )
        {
            GameCode = gameCode;
            FirstPrize = firstPrize;
            ConsolationPrize = consolationPrize;
            LobbyOpensAt = lobbyOpensAt;
            EndAt = endAt;
            IsRecurring = isRecurring;
            RecurringPattern = recurringPatter;
        }
    }
}
