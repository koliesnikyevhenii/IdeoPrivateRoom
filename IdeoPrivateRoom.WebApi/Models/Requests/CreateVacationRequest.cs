namespace IdeoPrivateRoom.WebApi.Models.Requests;

public class CreateVacationRequest
{
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
