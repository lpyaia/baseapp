using BaseApp.Application.CommandSide.Commands;
using BaseApp.Application.Dtos;
using BaseApp.Infra.CrossCutting.Identity.Authentication;
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
        public async Task<ActionResult<Token>> Post(AccessCredentialsDto credentials)
        {
            var loginCommand = new LoginCommand(credentials.UserName,
                credentials.Password,
                credentials.RefreshToken,
                credentials.GrantType);

            var token = await _mediator.Send(loginCommand);

            if (!token.IsAuthenticated)
                return Unauthorized(token);

            return Ok(token);
        }
    }
}