/*
 * All .NET web applications start as a console app.
 * We are using TOP-LEVEL statements which is c#10 feature. Hence, we don't see MAIN method here.
 * WebApplication is the class which is used build and run .NET web applications.
 * Builder pattern is used to create WebApplication instance. (This is a common .NET pattern which delays complex object creation till configuration is finished)
 */

using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

/*
 * Essentially builder.Services(IServiceCollection) is a place where developers register their services/classes for DI.
 * You can add services(classes required for your app) to your app before calling Build() method on it, because once you call build, DI gets constructed, you can't register more services. 
 * These services will be resolved automatically when required.
 */
/*
 * WHY USE DELEGATES IN EXTENSION METHODS RATHER THAN C# OBJECT (In this case HttpLoggingOptions)?
 * 1) If developers have to pass C# objects, that means they are manually constructing that object. This defeats the purpose of DI.
 * 2) If the intended C# object has other dependencies, developers can't use DI container(as it is not constructed yet) to resolve those dependencies and they will have to manually construct those dependencies as well.
 */
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
});

/*
 * After you call Build(), you can't register anymore services.
 * This is the point where DI container is actually created.
 * From here on framework knows how to construct each service/classes from their dependencies.
 */
var app = builder.Build();

//By default, RoutingMiddleware is added at the start of the middleware pipeline.

/*
 * WebApplication.Environment is set after Build() method is called.
 * Environment exposes several properties like 'ContentRootPath', 'WebRootPath' & 'EnvironmentName'
 * 'EnvironmentName' is set externally like Environment Variables.
 */
if (app.Environment.IsDevelopment())
{
    /*
     * After building WebApplication, developers can add middlewares to the WebApplication pipeline.
     * In middleware, developers can analyze and make change to HTTP request (HttpContext object).
     */
    app.UseHttpLogging();   
}

//By default, EndpointMiddleware is added at the end of the middleware pipeline.

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