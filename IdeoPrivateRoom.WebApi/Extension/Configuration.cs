using Microsoft.EntityFrameworkCore;
using IdeoPrivateRoom.WebApi.Services.Interfaces;
using IdeoPrivateRoom.WebApi.Services;
using IdeoPrivateRoom.WebApi.Mapping;
using IdeoPrivateRoom.DAL.Data;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using IdeoPrivateRoom.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using IdeoPrivateRoom.WebApi.Models.Options;
using IdeoPrivateRoom.WebApi.Configurations;
using FluentValidation;

namespace IdeoPrivateRoom.WebApi.Extension;

public static class Configuration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        //builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
          //.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
          //.EnableTokenAcquisitionToCallDownstreamApi(new string[] { "https://graph.microsoft.com/.default" })
          //.AddMicrosoftGraph(builder.Configuration.GetSection("GraphApi"))
          //.AddInMemoryTokenCaches();

        builder.Services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<VocationsListSettings>(builder.Configuration.GetSection(nameof(VocationsListSettings)));

        var corsOptions = builder.Configuration.GetSection(CorsOptions.Section).Get<CorsOptions>();
        var allowedOrigins = corsOptions?.AllowedOrigins?.Split(',') ?? [];

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(allowedOrigins);
            });
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("IdeoPrivateRoomDB")));

        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors();
    }
}
