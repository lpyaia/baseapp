namespace BaseApp.Infrastructure.Identity.Authentication
{
    public class AccessCredentials
    {
        public string? UserName { get; }

        public string? Password { get; }

        public string? RefreshToken { get; }

        public string? GrantType { get; }

        public AccessCredentials(string? userName,
            string? password,
            string? refreshToken,
            string? grantType)
        {
            UserName = userName;
            Password = password;
            RefreshToken = refreshToken;
            GrantType = grantType;
        }
    }
}