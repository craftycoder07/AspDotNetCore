using WebApplication1;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
 */
app.MapGet("/fruit", () => FruitHandler.GetFruits().Any() ? Results.Ok(FruitHandler.GetFruits()) : Results.NotFound());
app.MapGet("/fruit/{id}", (int id) => FruitHandler.GetFruits().Any(x => x.Id == id) ? Results.Ok(FruitHandler.GetFruit(id)) : Results.NotFound());
app.MapPost("/fruit", (Fruit fruit) => FruitHandler.AddFruit(fruit));
app.MapDelete("/fruit/{id}", (int id) =>
{
    if (FruitHandler.GetFruits().Any(x => x.Id == id))
    {
        FruitHandler.RemoveFruit(id);
        Results.Ok();
    }
    else
    {
        Results.NotFound();
    }
});


app.Run();