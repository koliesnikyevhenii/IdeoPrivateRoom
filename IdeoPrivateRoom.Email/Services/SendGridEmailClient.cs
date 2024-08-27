using IdeoPrivateRoom.Email.Models;
using IdeoPrivateRoom.Email.Models.Options;
using IdeoPrivateRoom.Email.Services.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.Json;

namespace IdeoPrivateRoom.Email.Services;
public class SendGridEmailClient : IEmailClient
{
    private readonly IOptionsMonitor<SendGridOptions> _configAccessor;
    private readonly ILogger<SendGridEmailClient> _logger;

    public SendGridEmailClient(
        IOptionsMonitor<SendGridOptions> configAccessor,
        ILogger<SendGridEmailClient> logger)
    {
        _configAccessor = configAccessor;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(MailMessage emailModel)
    {
        var typeName = nameof(SendGridEmailClient);
        var apiKey = _configAccessor.CurrentValue.ApiKey;
        var sender = _configAccessor.CurrentValue.SenderEmail;
        var client = new SendGridClient(apiKey);

        var msg = new SendGridMessage
        {
            From = new EmailAddress(sender),
            Subject = emailModel.Subject,
        };

        if (!emailModel.Recipients?.Any() ?? true)
        {
            _logger.LogError("{typeName}: the email has no recipients", typeName);
            return false;
        }
        
        msg.AddTos(emailModel.Recipients.Select(x => new EmailAddress(x)).ToList());

        if (emailModel.CcRecipients?.Any() ?? false)
        {
            msg.AddCcs(emailModel.CcRecipients.Select(x => new EmailAddress(x)).ToList());
        }

        if (!string.IsNullOrWhiteSpace(emailModel.Body))
        {
            msg.AddContent(MimeType.Html, emailModel.Body);
        }

        if (emailModel.Attachments?.Any() ?? false)
        {
            foreach (var attachment in emailModel.Attachments)
            {
                if (string.IsNullOrWhiteSpace(attachment.FileName))
                {
                    _logger.LogDebug("{typeName}: attachment filename is null or empty, skipping attachment", typeName);
                    continue;
                }

                if (attachment.Stream != null)
                {
                    await msg.AddAttachmentAsync(attachment.FileName, attachment.Stream);
                }
                else if (!string.IsNullOrWhiteSpace(attachment.Content))
                {
                    msg.AddAttachment(attachment.FileName, attachment.Content, attachment.Type, attachment.Disposition, attachment.ContentId);
                }
            }
        }

        msg.MailSettings = new MailSettings
        {
            SandboxMode = new SandboxMode
            {
                Enable = _configAccessor.CurrentValue.SandboxMode
            }
        };

        try
        {
            var recipients = emailModel.Recipients != null ? string.Join(',', emailModel.Recipients) : null;
            _logger.LogInformation("{typeName}: sending email with subject {subject} to recipients {recipients}",
                typeName, msg.Subject, recipients);

            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            _logger.LogInformation("{typeName}: response - {response}", typeName, JsonSerializer.Serialize(response));

            return response != null && response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{typeName}: unexpected exception occured - {exception}", typeName, ex.Message);
            return false;
        }
    }
}
