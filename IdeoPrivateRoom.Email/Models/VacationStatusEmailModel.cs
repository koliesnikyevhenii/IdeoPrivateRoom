namespace IdeoPrivateRoom.Email.Models;

public class VacationStatusEmailModel
{
    public string Recipient { get; set; } = default!;
    public string RecipientFullName { get; set; } = default!;
    public bool IsApproved { get; set; }
    public string ContactEmail { get; set; } = default!;

    public DateTime VacationDateStart { get; set; }
    public DateTime VacationDateEnd { get; set; }

    //TODO: verify if we need notes/attachments
    public string? Notes { get; set; }
}
