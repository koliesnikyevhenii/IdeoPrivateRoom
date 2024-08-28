using IdeoPrivateRoom.Email.Models;
using IdeoPrivateRoom.Email.Models.Options;
using IdeoPrivateRoom.Email.Models.Templates;
using IdeoPrivateRoom.Email.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace IdeoPrivateRoom.Email.Services;

public class EmailService : IEmailService
{
    private readonly IEmailClient _emailClient;
    private readonly IViewRenderer _viewRenderer;
    private readonly IOptions<EmailTemplateOptions> _emailTamplateOptions;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IEmailClient emailClient, IViewRenderer viewRenderer, IOptions<EmailTemplateOptions> emailTamplateOptions, ILogger<EmailService> logger)
    {
        _emailClient = emailClient;
        _viewRenderer = viewRenderer;
        _emailTamplateOptions = emailTamplateOptions;
        _logger = logger;
    }

    public async Task<bool> SendVacationRequestEmail(VacationRequestEmailModel model)
    {
        //TODO: include parameters for approve/reject links
        var vacationRequestTemplateModel = new VacationRequestTemplateModel
        {
            RequestorName = model.RequestorFullName,
            RequestorEmail = model.RequestorEmail,
            VacationDateStart = model.VacationDateStart,
            VacationDateEnd = model.VacationDateEnd,
            Notes = model.Notes
        };

        var htmlString = await _viewRenderer.RenderToStringAsync(_emailTamplateOptions.Value.VacationRequestTemplatePath, vacationRequestTemplateModel);

        var mailMessage = new MailMessage
        {
            Recipients = model.Recipients,
            CcRecipients = model.CcRecipients,
            Subject = $"Vacation Request - {model.RequestorFullName}", //TODO: verify subject format
            Body = htmlString,
        };

        return await _emailClient.SendEmailAsync(mailMessage);
    }

    public async Task<bool> SendVacationStatusEmail(VacationStatusEmailModel model)
    {
        var vacationStatusTemplateModel = new VacationStatusTemplateModel
        {
            RecipientName = model.RecipientFullName,
            VacationDateStart = model.VacationDateStart,
            VacationDateEnd = model.VacationDateEnd,
            IsApproved = model.IsApproved,
            Notes = model.Notes
        };

        var htmlString = await _viewRenderer.RenderToStringAsync(_emailTamplateOptions.Value.VacationStatusTemplatePath, vacationStatusTemplateModel);

        var mailMessage = new MailMessage
        {
            Recipients = new List<string> { model.Recipient },
            Subject = $"Vacation Status - {model.RecipientFullName}", //TODO: verify subject format
            Body = htmlString,
        };

        return await _emailClient.SendEmailAsync(mailMessage);
    }

}
