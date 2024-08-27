namespace IdeoPrivateRoom.Email.Models.Templates;

public class VacationRequestTemplateModel
{
    public string RequestorName { get; set; } = default!;
    public string? RequestorEmail { get; set; } = default!;
    public DateTime VacationDateStart { get; set; }
    public DateTime VacationDateEnd { get; set; }
    public string? Notes { get; set; }
}
