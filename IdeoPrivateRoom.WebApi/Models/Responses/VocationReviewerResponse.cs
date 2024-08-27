using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Models.Responses;

public class VocationReviewerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public ApprovalStatus ApprovalStatus { get; set; }
}
