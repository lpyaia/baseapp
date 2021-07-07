namespace BaseApp.Infra.CrossCutting.Identity.Authentication
{
    public static class GrantType
    {
        public static string Password => "password";

        public static string RefreshToken => "refresh_token";
    }
}