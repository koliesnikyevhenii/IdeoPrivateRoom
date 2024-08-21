using IdeoPrivateRoom.WebApi.Data.Entities;
using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Models.Dtos;

public class VocationRequestDto
{
    public Guid Id { get; set; }
    public VocationUserDto User { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public ApprovalStatus VocationStatus { get; set; }
    public IEnumerable<VocationUserApprovalDto> UserApprovalResponses { get; set; }
}
