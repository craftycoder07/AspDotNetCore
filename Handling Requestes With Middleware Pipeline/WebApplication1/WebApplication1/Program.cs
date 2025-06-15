using Microsoft.AspNetCore.Diagnostics;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

/*
 * Middleware in ASP.NET is nothing but a c# class that can handle HTTP request.
 * .NET convention is to use Use* extension methods to add middlewares to the pipeline.
 * Under the hood that extension method calls another method to add middleware to the pipeline
 * In following example, I have added WelcomePage middleware using extension method.
 * Which under the hood calls app.UseMiddleware<WelcomePageMiddleware> to add middleware to the pipeline.
 */
app.UseWelcomePage("/");
//app.UseMiddleware<WelcomePageMiddleware>();

app.Run();