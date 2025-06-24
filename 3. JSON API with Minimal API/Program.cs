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
app.MapPost("/fruit", (Fruit fruit) => FruitHandler.AddFruit(fruit));
app.MapGet("/fruit", () => FruitHandler.GetFruits());
app.MapGet("/fruit/{id}", (int id) => FruitHandler.GetFruit(id));
app.MapDelete("/fruit/{id}", (int id) => FruitHandler.RemoveFruit(id));


app.Run();