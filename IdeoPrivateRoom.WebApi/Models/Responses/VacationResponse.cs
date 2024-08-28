using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Models.Responses;

public class VacationResponse
{
    public Guid Id { get; set; }
    public string? Comment { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime CreatedDate { get; set; }
    public ApprovalStatus Status { get; set; }
    public VacationUserResponse User { get; set; }
    public List<VacationReviewerResponse> Reviewers { get; set; } = [];
}
