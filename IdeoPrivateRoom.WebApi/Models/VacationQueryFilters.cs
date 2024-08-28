namespace IdeoPrivateRoom.WebApi.Models;

public class VacationQueryFilters
{
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? UserIds { get; set; }
    public string? Statuses { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}
