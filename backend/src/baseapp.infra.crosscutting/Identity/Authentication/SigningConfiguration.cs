using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BaseApp.Infra.CrossCutting.Identity.Authentication
{
    public class SigningConfiguration
    {
        public SecurityKey Key { get; }

        public SigningCredentials SigningCredentials { get; }

        public SigningConfiguration(TokenConfiguration tokenConfig)
        {
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.SecretJwtKey));
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}