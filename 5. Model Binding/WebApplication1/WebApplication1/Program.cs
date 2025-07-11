var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*
 * BINDING LOCATION
 * In .NET, model binding happens INSIDE endpoint middleware BEFORE filter pipeline.
 */

/*
 * BINDING SOURCES
 * .NET can use 6 sources for model binding
 * 1) Http request route parameter
 * 2) Http request Query string
 * 3) Http headers
 * 4) Http body
 */

/*
 * BINDING SIMPLE TYPES TO A REQUEST
 * 
 */
app.MapGet("/", () => "Hello World!");

app.Run();