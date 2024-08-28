namespace IdeoPrivateRoom.WebApi.Models.Requests;

public class CreateVocationRequest
{
    public string UserId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
