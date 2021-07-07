using BaseApp.Infra.CrossCutting.Identity.Authentication;
using MediatR;

namespace BaseApp.Application.CommandSide.Commands
{
    public class LoginCommand : IRequest<Token>
    {
        public string? UserName { get; }

        public string? Password { get; }

        public string? RefreshToken { get; }

        public string? GrantType { get; }

        public LoginCommand(string? userName, string? password, string? refreshToken, string? grantType)
        {
            UserName = userName;
            Password = password;
            RefreshToken = refreshToken;
            GrantType = grantType;
        }
    }
}