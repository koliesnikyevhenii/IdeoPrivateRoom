namespace IdeoPrivateRoom.WebApi.Models.Requests;

public class UpdateVocationRequest
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public string? Comment { get; set; }
    public string? Status { get; set; }
}
