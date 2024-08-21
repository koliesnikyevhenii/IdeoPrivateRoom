using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Models.Responses;

public class VocationResponse
{
    public Guid Id { get; set; }
    public string? Comment { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime CreatedDate { get; set; }
    public ApprovalStatus Status { get; set; }
    public VocationUserResponse User { get; set; }
    public IEnumerable<VocationUserApprovalResponse> UserApprovalResponses { get; set; }
}
