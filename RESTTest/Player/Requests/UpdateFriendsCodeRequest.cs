namespace RESTTest.Player.Requests
{
    public class UpdateFriendsCodeRequest
    {
        public string FriendCode { get; set; }

        public UpdateFriendsCodeRequest(string friendCode)
        {
            FriendCode = friendCode;
        }
    }
}
