namespace RESTTest.Leaderboard.Requests
{
    public class SubscribeRequest
    {
        public string Pin { get; set; }

        public SubscribeRequest(string pin) => Pin = pin;
    }
}
