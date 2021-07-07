using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BaseApp.Api.Controllers
{
    [Route("api/Test")]
    [ApiController]
    [Authorize(Policy = "Bearer", Roles = "Admin, Customer")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly ILogger<TestController> _logger;

        public TestController(IMediator mediatr, ILogger<TestController> logger)
        {
            _mediatr = mediatr;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetAdmin()
        {
            return Ok(User.Identity!.Name);
        }
    }
}