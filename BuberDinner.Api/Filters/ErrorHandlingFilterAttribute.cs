using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        // below for the second approach of error handling

        // public override void OnException(ExceptionContext context)
        // {
        //     var exception = context.Exception;
        //     context.Result = new ObjectResult(new { error = "An error occured while processing your request." })
        //     {
        //         StatusCode = 500
        //     };
        //     context.ExceptionHandled = true;
        // }


        // this is belonge for the third approach of error handling
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var problemDetails = new ProblemDetails
            {
                Type="https://tools.ietf.org/html/rfc7231#section-6.6.1", //optional
                Title = "An error occured while processing your request.",
                Status = (int)HttpStatusCode.InternalServerError,
            };
            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
    }
}