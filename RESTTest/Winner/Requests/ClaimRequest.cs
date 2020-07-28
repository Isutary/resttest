namespace RESTTest.Winner.Requests
{
    public class ClaimRequest
    {
        public string Status { get; set; }

        public ClaimRequest(string status)
        {
            Status = status;
        }
    }
}
