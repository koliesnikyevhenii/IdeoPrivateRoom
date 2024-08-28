using IdeoPrivateRoom.WebApi.Endpoints;
using IdeoPrivateRoom.WebApi.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

var app = builder.Build();

app.RegisterMiddlewares();
app.RegisterUserEndpoints();
app.RegisterVacationEndpoints();

app.Run();
