namespace BaseApp.Infra.CrossCutting.Identity.Authentication
{
    public class TokenConfiguration
    {
        public string SecretJwtKey { get; set; } = default!;

        public string Audience { get; set; } = default!;

        public string Issuer { get; set; } = default!;

        public int Seconds { get; set; }

        public int FinalExpiration { get; set; }
    }
}