namespace IdeoPrivateRoom.WebApi.Models;

public class VocationQueryFilters
{
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? UserIds { get; set; }
    public string? Statuses { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 7;
}
