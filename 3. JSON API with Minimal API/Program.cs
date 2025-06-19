var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Parameterized routes
app.MapGet("/firstname/{firstName}", (string firstName) => $"Hello {firstName}!");

//Nullable parameterized routes
app.MapGet("/id/{id?}", (int? id) => id == null ? "You did not pass any id" : $"Your passed: {id}");

app.Run();