/*
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
*/

using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication()
                    .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();

    // This is the second & Third approaches options => options.Filters.Add<ErrorHandlingFilterAttribute>()
    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());

}

var app = builder.Build();
{
    //    app.UseMiddleware<ErrorHandlingMiddleware>(); // this is the first approach

    //below for fouth approach for error handling, use with errorcontroller
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}



