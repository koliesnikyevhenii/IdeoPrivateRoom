using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Models.Dtos;

public class VocationUserApprovalDto
{
    public Guid Id { get; set; }
    public VocationUserDto User { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }
}
