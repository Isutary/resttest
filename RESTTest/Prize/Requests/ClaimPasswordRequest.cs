namespace RESTTest.Prize.Requests
{
    public class ClaimPasswordRequest
    {
        public string ClaimRequestId { get; set; }
        public string FirstName { get; set; }
        public string PaypalAddress { get; set; }
        public string PhoneNumber { get; set; }

        public ClaimPasswordRequest(string claimRequestId, string firstName, string paypalAddress, string phoneNumber)
        {
            ClaimRequestId = claimRequestId;
            FirstName = firstName;
            PaypalAddress = paypalAddress;
            PhoneNumber = phoneNumber;
        }

    }
}
