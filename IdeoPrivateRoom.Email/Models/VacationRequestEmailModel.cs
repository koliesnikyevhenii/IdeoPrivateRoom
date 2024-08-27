namespace IdeoPrivateRoom.Email.Models;

public class VacationRequestEmailModel
{
    public List<string> Recipients { get; set; } = new();
    public List<string>? CcRecipients { get; set; }

    public string RequestorFullName { get; set; } = default!;
    public string? RequestorEmail { get; set; }

    public DateTime VacationDateStart { get; set; }
    public DateTime VacationDateEnd { get; set; }

    //TODO: verify if we need notes/attachments
    public string? Notes { get; set; }
}
