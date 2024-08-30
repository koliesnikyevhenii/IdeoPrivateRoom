namespace IdeoPrivateRoom.WebApi.Models.Requests;

public class UpdateApprovalStatusRequest
{
    public string UserId { get; set; } = string.Empty;
    public string VacationId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
