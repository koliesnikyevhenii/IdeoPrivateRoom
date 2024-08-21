﻿/*using IdeoPrivateRoom.WebApi.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Repositories;*/
using Microsoft.EntityFrameworkCore;
using IdeoPrivateRoom.WebApi.Services.Interfaces;
using IdeoPrivateRoom.WebApi.Services;
using IdeoPrivateRoom.WebApi.Mapping;
using IdeoPrivateRoom.DAL.Data;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using IdeoPrivateRoom.DAL.Repositories;

namespace IdeoPrivateRoom.WebApi.Extension;

public static class Configuration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("IdeoPrivateRoomDB")));

        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IVocationRepository, VocationRepository>();
        builder.Services.AddScoped<IVocationService, VocationService>();
    }

    public static void RegisterMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ApplyMigrations();
            using var serviceScope = app.Services.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            DBInitializer.Seed(context);
        }

        app.UseHttpsRedirection();

        app.UseCors(builder => builder.WithOrigins("http://localhost:8080", "http://localhost:4200"));
    }
}
