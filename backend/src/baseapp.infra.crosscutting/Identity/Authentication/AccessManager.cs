using BaseApp.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BaseApp.Infra.CrossCutting.Identity.Authentication
{
    public class AccessManager
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly SigningConfiguration _signingConfiguration;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IMemoryCache _cache;

        public AccessManager(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            SigningConfiguration signingConfiguration,
            TokenConfiguration tokenConfiguration,
            IMemoryCache cache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _cache = cache;
        }

        public async Task<AuthenticationResult> ValidateCredentials(AccessCredentials credentials)
        {
            IdentityUser? userIdentity = null;

            var isValidCredential = false;

            if (credentials.UserName.HasValue())
            {
                userIdentity = await _userManager.FindByNameAsync(credentials.UserName);

                if (userIdentity != null &&
                    credentials.GrantType == GrantType.Password)
                {
                    isValidCredential = await LoginByUserPassword(credentials, userIdentity);
                }

                else if (credentials.GrantType == GrantType.RefreshToken &&
                    credentials.RefreshToken.HasValue())
                {
                    isValidCredential = LoginByRefreshToken(credentials);
                }
            }

            return new AuthenticationResult(isValidCredential, userIdentity);
        }

        public async Task<Token> GenerateToken(IdentityUser userIdentity)
        {
            var creationDate = DateTime.Now;
            var expirationDate = creationDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

            ClaimsIdentity identity = await GenerateClaims(userIdentity);
            Token token = GenerateToken(identity, creationDate, expirationDate);
            RefreshTokenData refreshToken = GenerateRefreshToken(userIdentity, token);

            SaveRefreshTokenInCache(token, refreshToken);

            return token;
        }

        private static RefreshTokenData GenerateRefreshToken(IdentityUser userIdentity, Token token)
        {
            var refreshToken = new RefreshTokenData
            (
                userIdentity.UserName,
                token.RefreshToken
            );

            return refreshToken;
        }

        private void SaveRefreshTokenInCache(Token result, RefreshTokenData refreshToken)
        {
            var expirationInSeconds = TimeSpan.FromSeconds(_tokenConfiguration.FinalExpiration);

            var cacheOptions = new MemoryCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(expirationInSeconds);

            _cache
                .Set(result.RefreshToken, JsonSerializer.Serialize(refreshToken), cacheOptions);
        }

        private Token GenerateToken(ClaimsIdentity identity, DateTime creationDate, DateTime expirationDate)
        {
            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate
            });

            return new
            (
                true,
                creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                handler.WriteToken(securityToken),
                Guid.NewGuid().ToString().Replace("-", string.Empty),
                "OK"
            );
        }

        private async Task<ClaimsIdentity> GenerateClaims(IdentityUser userIdentity)
        {
            var claims = new ClaimsIdentity
            (
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.Email, userIdentity.NormalizedEmail),
                    new Claim(JwtRegisteredClaimNames.UniqueName, userIdentity.NormalizedUserName),
                    new Claim("userid", userIdentity.Id)
                }
            );

            var roles = (await _userManager.GetRolesAsync(userIdentity)).ToList();

            roles.ForEach(role => claims.AddClaim(new Claim(ClaimTypes.Role, role)));

            return claims;
        }

        private async Task<bool> LoginByUserPassword(AccessCredentials credentials, IdentityUser userIdentity)
        {
            var login = await _signInManager
                .CheckPasswordSignInAsync(userIdentity, credentials.Password, false);

            return login.Succeeded;
        }

        private bool LoginByRefreshToken(AccessCredentials credentials)
        {
            RefreshTokenData? refreshTokenData = null;

            var currentToken = _cache.Get<string>(credentials.RefreshToken);

            if (currentToken.HasValue())
                refreshTokenData = JsonSerializer.Deserialize<RefreshTokenData>(currentToken);

            return refreshTokenData != null &&
                credentials.UserName == refreshTokenData.UserId &&
                credentials.RefreshToken == refreshTokenData.RefreshToken;
        }
    }
}