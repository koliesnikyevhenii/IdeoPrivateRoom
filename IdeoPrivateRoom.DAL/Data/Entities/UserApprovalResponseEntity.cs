namespace IdeoPrivateRoom.DAL.Data.Entities;

public class UserApprovalResponseEntity
{
    public Guid Id { get; set; }
    public string ApprovalStatus { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public Guid VacationRequestId { get; set; }
    public VacationRequestEntity VacationRequest { get; set; } = null!;
}
