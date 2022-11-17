using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

using MediatR;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using Microsoft.AspNetCore.Authorization;

namespace BuberDinner.Api.Controllers
{

    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {

       // private readonly IMediator _mediator;
        private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request.firstName,request.lastName,request.email,request.password);
            var authResult = await _mediator.Send(command);

            var response = new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.email,request.password);
            var authResult =await _mediator.Send(query);

            var response = new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);

            return Ok(response);
        }
    }
}