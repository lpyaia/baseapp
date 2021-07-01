namespace BaseApp.Infrastructure.Identity.Authentication
{
    public class Token
    {
        public static Token NotAuthorizedToken => new(false,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            InvalidCredentialsMessage);

        private const string InvalidCredentialsMessage = "Authentication error: invalid credentials.";

        public bool IsAuthenticated { get; }

        public string Created { get; }

        public string Expiration { get; }

        public string AccessToken { get; }

        public string RefreshToken { get; }

        public string Message { get; }

        public Token(bool isAuthenticated,
            string created,
            string expiration,
            string accessToken,
            string refreshToken,
            string message)
        {
            IsAuthenticated = isAuthenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Message = message;
        }
    }
}