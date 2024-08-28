namespace IdeoPrivateRoom.Email.Models.Templates;

public class VacationStatusTemplateModel
{
    public string RecipientName { get; set; } = default!;
    public DateTime VacationDateStart { get; set; }
    public DateTime VacationDateEnd { get; set; }
    public bool IsApproved { get; set; }
    public string? ContactEmail { get; set; }
    public string? Notes { get; set; }
}
