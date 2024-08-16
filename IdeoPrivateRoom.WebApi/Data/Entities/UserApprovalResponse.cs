using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Data.Entities;

public class UserApprovalResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid VocationRequestId { get; set; }
    public VocationRequest VocationRequest { get; set; } = null!;
    public ApprovalStatus ApprovalStatus { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
