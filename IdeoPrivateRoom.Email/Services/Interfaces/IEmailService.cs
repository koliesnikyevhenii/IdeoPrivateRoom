using IdeoPrivateRoom.Email.Models;

namespace IdeoPrivateRoom.Email.Services.Interfaces;

public interface IEmailService
{
    Task<bool> SendVacationRequestEmail(VacationRequestEmailModel model);
    Task<bool> SendVacationStatusEmail(VacationStatusEmailModel model);
}
