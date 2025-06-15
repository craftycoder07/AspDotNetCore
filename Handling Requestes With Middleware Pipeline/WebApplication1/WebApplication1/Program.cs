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

/*
 * There is a middleware to serve static files as well (Images, Javascript, CSS etc.)
 * For this one you need to have 'wwwroot' folder in your application.
 * To access the file you have to use path /filename.extension
 * If file is available, it will be served else client will get 404(not found) error
 */
app.UseStaticFiles();

app.Run();