using IdeoPrivateRoom.WebApi.Data;
using IdeoPrivateRoom.WebApi.Data.Entities;
using IdeoPrivateRoom.WebApi.Extension;
using IdeoPrivateRoom.WebApi.Repositories;
using IdeoPrivateRoom.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdeoPrivateRoomDB")));

builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
    DBInitializer.Seed(app);
}

app.UseHttpsRedirection();

app.MapGet("/users", async (IUserRepository userRepository) =>
{
    return await userRepository.Get();
})
.WithName("GetUsers")
.WithOpenApi();

app.MapPost("/users", async (User user, IUserRepository userRepository) =>
{
    return await userRepository.Add(user);
})
.WithName("PostUser")
.WithOpenApi();

app.Run();
