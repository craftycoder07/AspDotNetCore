var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/* ************************
 * WHAT IS ROUTING IN .NET?
 * Routing is process of selecting which endpoints to execute based on http request received. Routing is very powerful in .NET Core.
 */

/* ************************
 * WHY ROUTING MIDDLEWARE WAS CREATED (SEPARATED)?
 * In .NET Core 2.0 & 2.1 routing was part of Razor pages and MVC framework.
 * Hence, other parts of the program were not able to use routing. (e.g. Authorization middleware now can use information stored by routing middleware on 'HttpContext' to figure out if use is authorized to execute that endpoint)
 * Since then routing is separated into a middleware called 'Routing Middleware'. It is automatically added if not directly mentioned in the code when using endpoints.
 */

/* ************************
 * HOW ROUTING WORKS IN .NET?
 * Whenever http request comes to .NET app, it passes through routing middleware. This middleware maintains list of all the endpoints with its 'PATH' called 'Route Template'
 * If http request 'PATH' matches with list of paths available, this middleware updates 'HttpContext' object with which endpoint to execute.
 * Then request moves to next middleware until it reaches to endpoint middleware where the actual handler is executed.
 */

app.UseRouting();


/* ************************
 * SIMPLE ROUTE TEMPLATE SYNTAX
 * Route Templates consists of 'Segments'.
 * Route Templates is divided into segments with separators. You can use any separator but '/' is a standard.
 * There are two types of segments. 'Literal' or 'Parameter'.
 * 'Literal segment' is constant (case insensitive) where 'Parameter Segment' is variable.
 * In following example 'product' is literal while 'category' and 'name' are 'Parameter' (required) segment
 */
app.MapGet("/product/{category}/{name}", () => "Hello World!");


/* ***********************
 * ADDING DEFAULT PARAMETERS TO ROUTE TEMPLATE
 * You define 'Default' parameter with '=' with default value. If user don't pass any value, default value is taken.
 * Route parameter '/product' and '/product/all' will point to same handler.
 */
app.MapGet("/product/{name=all}", () => "Hello World!");

/* ***********************
 * ADDING OPTIONAL PARAMETERS TO ROUTE TEMPLATE
 * You define 'Optional' parameter with '?' with optional value. If user don't pass any value, value of the parameter will be null.
 */
app.MapGet("/product/grocery/{id?}", () => "Hello World!");

/* ***********************
 * COMBINE DEFAULT AND OPTIONAL PARAMETER
 * You can combine 'Default' & 'Optional' parameter but optional parameter has to be the last one.
 * Plus default parameter needs to be passed if used wants to pass optional parameter.
 */
app.MapGet("/product/stationary/{name=all}/{id?}", () => "Hello World!");

app.Run();