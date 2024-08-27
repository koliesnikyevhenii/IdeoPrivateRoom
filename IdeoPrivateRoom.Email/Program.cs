using IdeoPrivateRoom.Email.Endpoints;
using IdeoPrivateRoom.Email.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

builder.Services.AddMvcCore().AddRazorViewEngine();

var app = builder.Build();

app.RegisterMiddlewares();
app.RegisterEmailEndpoints();

app.Run();