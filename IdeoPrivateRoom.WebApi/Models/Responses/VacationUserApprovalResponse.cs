using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Models.Responses;

public class VacationUserApprovalResponse
{
    public Guid Id { get; set; }
    public VacationUserResponse User { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }
}
