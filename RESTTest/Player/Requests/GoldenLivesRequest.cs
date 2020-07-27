namespace RESTTest.Player.Requests
{
    public class GoldenLivesRequest
    {
        public string GoldenLives { get; set; }

        public GoldenLivesRequest(string goldenLives)
        {
            GoldenLives = goldenLives;
        }
    }
}
