namespace BaseApp.Application.Dtos
{
    public class AccessCredentialsDto
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? RefreshToken { get; set; }

        public string? GrantType { get; set; }
    }
}