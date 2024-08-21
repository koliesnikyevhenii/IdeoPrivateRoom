using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Models.Responses;

public class VocationUserApprovalResponse
{
    public Guid Id { get; set; }
    public VocationUserResponse User { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }
}
