using IdeoPrivateRoom.Email.Models;

namespace IdeoPrivateRoom.Email.Services.Interfaces;

public interface IEmailClient
{
    Task<bool> SendEmailAsync(MailMessage emailDetails);
}
