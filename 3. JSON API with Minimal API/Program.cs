using WebApplication1;

var builder = WebApplication.CreateBuilder(args);
//To convert failed responses to ProblemDetails
builder.Services.AddProblemDetails();

var app = builder.Build();

//If you use 'AddProblemDetails', exception related middlewares automatically converts responses to ProblemDetails
if (!app.Environment.IsDevelopment())
{ 
    app.UseExceptionHandler();
}

//This will convert failed response statues to ProblemDetails. (This does not handle exceptions) 
app.UseStatusCodePages();

//Parameterized routes
app.MapGet("/firstname/{firstName}", (string firstName) => $"Hello {firstName}!");

//Nullable parameterized routes
app.MapGet("/id/{id?}", (int? id) => id == null ? "You did not pass any id" : $"Your passed: {id}");

//Http post request. Used for 'CREATE' operation.
app.MapPost("/person", (Person person) => $"Hello {person.FirstName} {person.LastName}!");

//Http put request. Used for 'REPLACE' operation.
app.MapPut("/person", (Person person) => $"Hello {person.FirstName} {person.LastName}!");

//Http patch request. Used for 'UPDATE' operation.
app.MapPatch("/person", (Person person) => $"Hello {person.FirstName} {person.LastName}!");

//Http delete request. Used for 'DELETE' operation.
app.MapDelete("/person/{id}", (int id) => $"Hello person with id={id} is deleted!");

//Practice examples of HTTP request verbs
/*
 * Use IResult to return more descriptive results from API.
 * In general, you can return following from the endpoint
 * 1) void -> returns 200 status code with empty body
 * 2) string -> returns 200 status code with HTTP response content-type as text/pain
 * 3) T -> returns 200 status code with object T serialized as JSON object with HTTP response content-type as application/json
 * 4) IResult -> Manually send status codes with methods provided by their implementations. (Result & TypedResult). This is used to send mode descriptive status codes like NON-happy paths.
 * Result & TypedResult are static helper classes that provides static methods to return different status codes as per requirements.
 * Result returns IResult type whereas TypedResult returns actual type like OK<T>. Generic version is better for unit testing.
 * In addition, Result has ProblemDetail and ValidationProblem methods as well which help unify API responses for error status code in terms of ProblemDetails which is a web standard.
 */
app.MapGet("/fruit", () => FruitHandler.GetFruits().Any() ? Results.Ok(FruitHandler.GetFruits()) : Results.Problem(statusCode: 404)); //This is an example. Not an actual problem. Empty list can be sent.
app.MapGet("/fruit/{id}", (int id) => FruitHandler.GetFruits().Any(x => x.Id == id) ? Results.Ok(FruitHandler.GetFruit(id)) : Results.Problem(statusCode:404, title:"Fruit not found", detail:"Fruit with given id is not found."));
app.MapPost("/fruit", (Fruit fruit) =>
{
    if (FruitHandler.GetFruits().Any(x => x.Id == fruit.Id))
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            { "id", new[] { "A fruit with this id already exists" } }
        });
    }
    else
    {
        FruitHandler.AddFruit(fruit);
        return Results.Created();
    }
});
app.MapDelete("/fruit/{id}", (int id) =>
{
    if (FruitHandler.GetFruits().Any(x => x.Id == id))
    {
        FruitHandler.RemoveFruit(id);
        return Results.Ok();
    }
    else
    {
        return Results.Problem(statusCode: 404, title: "Fruit not found", detail: "Fruit with given id is not found.");
    }
});

app.MapGet("/Employee", () =>
{
    throw new Exception();
});

app.Run();