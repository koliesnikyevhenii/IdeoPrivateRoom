using IdeoPrivateRoom.Email.Models.Options;
using IdeoPrivateRoom.Email.Services;
using IdeoPrivateRoom.Email.Services.Interfaces;

namespace IdeoPrivateRoom.Email.Extensions;

public static class Configuration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var corsOptions = builder.Configuration.GetSection(CorsOptions.Section).Get<CorsOptions>();
        var allowedOrigins = corsOptions?.AllowedOrigins?.Split(',') ?? [];

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(allowedOrigins);
            });
        });

        builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("Email:SendGrid"));
        builder.Services.Configure<EmailTemplateOptions>(builder.Configuration.GetSection("Email:Templates"));

        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IEmailClient, SendGridEmailClient>();
        builder.Services.AddScoped<IViewRenderer, ViewRenderer>();
    }

    public static void RegisterMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            using var serviceScope = app.Services.CreateScope();
        }

        app.UseHttpsRedirection();

        app.UseCors();
    }
}
