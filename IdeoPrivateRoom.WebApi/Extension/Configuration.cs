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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation;

namespace IdeoPrivateRoom.WebApi.Extension;

public static class Configuration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
          

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AccessAsUser", policy =>
                policy.RequireClaim("scp", "access_as_user"));
        });

  
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<VacationsListSettings>(builder.Configuration.GetSection(nameof(VacationsListSettings)));

        var corsOptions = builder.Configuration.GetSection(CorsOptions.Section).Get<CorsOptions>();
        var allowedOrigins = corsOptions?.AllowedOrigins?.Split(',') ?? [];

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader();
            });
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("IdeoPrivateRoomDB")));

        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IVacationRepository, VacationRepository>();
        builder.Services.AddScoped<IVacationService, VacationService>();

        builder.Services.AddScoped<IUserApprovalVacationRepository, UserApprovalVacationRepository>();
        builder.Services.AddScoped<IUserApprovalVacationService, UserApprovalVacationService>();
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
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

      
    }
}
