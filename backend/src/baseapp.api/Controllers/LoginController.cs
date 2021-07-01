using BaseApp.Application.CommandSide.Commands;
using BaseApp.Application.Dtos;
using BaseApp.Infrastructure.Identity.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaseApp.Api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Token> Post(AccessCredentialsDto credentials)
        {
            var loginCommand = new LoginCommand(credentials.UserName,
                credentials.Password,
                credentials.RefreshToken,
                credentials.GrantType);

            return await _mediator.Send(loginCommand);
        }
    }
}