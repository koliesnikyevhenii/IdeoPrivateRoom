using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Models;

public class UserApprovalResponse
{
    public Guid Id { get; set; }
    public User User { get; set; } = null!;
    public VacationRequestEntity VacationRequest { get; set; } = null!;
    public ApprovalStatus ApprovalStatus { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
