namespace RESTTest.Player.Requests
{
    public class UpdateSettingsRequest
    {
        public string HandColor { get; set; }
        public string AllowMarketingPermissions { get; set; }
        public string AllowPushNotifications { get; set; }
        public string SoundDisabled { get; set; }

        public UpdateSettingsRequest(
            string handColor, 
            string allowMarketingPermissions, 
            string allowPushNotifications, 
            string soundDisabled)
        {
            HandColor = handColor;
            AllowMarketingPermissions = allowMarketingPermissions;
            AllowPushNotifications = allowPushNotifications;
            SoundDisabled = soundDisabled;
        }
    }
}
