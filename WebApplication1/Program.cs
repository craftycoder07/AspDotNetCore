/*
 * All .NET web applications start as a console app.
 * We are using TOP-LEVEL statements which is c#10 feature. Hence, we don't see MAIN method here.
 * WebApplication is the class which is used build and run .NET web applications.
 * Builder pattern is used to create WebApplication instance. (This is a common .NET pattern which delays complex object creation till configuration is finished)
 */

using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

//You can add services(classes required for your app) to your app  before calling Build() method on it.
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
});

var app = builder.Build();

/*
 * After building WebApplication, developers can add middlewares to the WebApplication pipeline.
 * In middleware, developers can analyze and make change to HTTP request.
 */
if (app.Environment.IsDevelopment())
    app.UseHttpLogging();

/*
 * You can add endpoints to your app using Map* methods after middleware pipeline.
 * These methods mainly take PATH and a delegate/handler to respond to HTTP request.
 * In following example I am using MapGet method which responds to HTTP GET request.
 */
app.MapGet("/", () => "Hello World!");

/*
 * You have to call Run() method on WebApplication instance in order to start listening to HTTP request.
 */
app.Run();