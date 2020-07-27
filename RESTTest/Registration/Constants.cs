namespace RESTTest.Registration
{
    public static class Constants
    {
        public const string PlayerID = "/941acd9e-4006-494c-9dbb-0a3dbd3d6050";
        public const string User = Common.Constants.Version + "/User";
        public const string Register = User + "/register";
        public const string Search = Register + "/search";
        public const string ProfilePicture = Register + PlayerID + "/profile-image";
        public const string DefaultProfilePicture = User + "/profile-image/default/DefaultImage";
    }
}
