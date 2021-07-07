using BaseApp.Application.CommandSide.Commands;
using BaseApp.Domain.Notifications;
using BaseApp.Infra.CrossCutting.Identity.Authentication;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApp.Application.CommandSide.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Token>
    {
        private readonly AccessManager _accessManager;
        private readonly INotificationContext _notificationContext;

        public LoginCommandHandler(AccessManager accessManager, INotificationContext notificationContext)
        {
            _accessManager = accessManager;
            _notificationContext = notificationContext;
        }

        public async Task<Token> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var accessCredentials = new AccessCredentials(request.UserName,
                request.Password,
                request.RefreshToken,
                request.GrantType);

            var authResult = await _accessManager.ValidateCredentials(accessCredentials);

            if (authResult.IsAuthenticated)
                return await _accessManager.GenerateToken(authResult.IdentityUser!);

            return Token.NotAuthorizedToken;
        }
    }
}