using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace BuberDinner.Api.Controllers
{
    [ApiController]
    [Route("error")]
    public class ErrorController : ControllerBase
    {

        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var(statusCode,message) = exception switch
            {
                DuplicateEmailException  =>(StatusCodes.Status409Conflict,"Email already exists."),
                //Exception => (StatusCodes.Status401Unauthorized,"Not Authorized"),
                _=>(StatusCodes.Status500InternalServerError,"An unexpected error occured."),
            };

            return Problem(statusCode : statusCode, title:message );
        }
    }
}