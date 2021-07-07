namespace BaseApp.Infra.CrossCutting.Identity.Authentication
{
    public class RefreshTokenData
    {
        public string RefreshToken { get; }

        public string UserId { get; }

        public RefreshTokenData(string refreshToken, string userId)
        {
            RefreshToken = refreshToken;
            UserId = userId;
        }
    }
}