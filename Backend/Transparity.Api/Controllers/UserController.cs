using Microsoft.AspNetCore.Mvc;
using Transparity.Application.Abstractions;
using Transparity.Application.Users.Commands;

namespace Transparity.Api.Controllers {
    [Route("api/user")]
    public class UserController : BaseController {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command) {
            var result = await _mediator.SendAsync(command);
            return Created(string.Empty, result);
        }
    }
}
