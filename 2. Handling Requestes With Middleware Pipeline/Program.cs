using Microsoft.AspNetCore.Diagnostics;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    /*
     * In development, you want to give details of the error in order to fix them.
     * This middleware gets automatically added in development environment.
     */
    app.UseDeveloperExceptionPage();
}
else
{
    /*
     * In production, you might want to divert user to more generic page.
     * This will rerun the middleware pipeline with PATH=/Error and execute that endpoint.
     */
    app.UseExceptionHandler("/Error");
}

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

app.MapGet("/Error", () => "Error occurred.");
app.MapGet("/HelloWorld", () => "Hello World!.");

app.Run();