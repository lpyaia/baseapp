using Microsoft.AspNetCore.Identity;

namespace BaseApp.Infrastructure.Identity.Authentication
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; }

        public IdentityUser? IdentityUser { get; }

        public AuthenticationResult(bool isAuthenticated,
            IdentityUser? identityUser)
        {
            IsAuthenticated = isAuthenticated;
            IdentityUser = identityUser;
        }
    }
}