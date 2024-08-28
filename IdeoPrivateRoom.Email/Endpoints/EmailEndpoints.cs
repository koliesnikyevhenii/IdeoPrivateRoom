using IdeoPrivateRoom.Email.Models;
using IdeoPrivateRoom.Email.Services.Interfaces;

namespace IdeoPrivateRoom.Email.Endpoints;

public static class EmailEndpoints
{
    public static void RegisterEmailEndpoints(this IEndpointRouteBuilder routes)
    {
        var email = routes.MapGroup("/api/email")
            .WithTags("Email");

        email.MapPost("/vacation-request", (IEmailService emailService, VacationRequestEmailModel model) =>
        {
            return emailService.SendVacationRequestEmail(model);
        })
        .WithOpenApi();

        email.MapPost("/vacation-status", (IEmailService emailService, VacationStatusEmailModel model) =>
        {
            return emailService.SendVacationStatusEmail(model);
        })
        .WithOpenApi();
    }
}
