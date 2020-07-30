namespace RESTTest.Leaderboard.Requests
{
    public class CreateLeaderboardRequest
    {
        public string Name { get; set; }

        public CreateLeaderboardRequest(string name) => Name = name;
    }
}
