namespace IdeoPrivateRoom.DAL.Data.Entities;

public class UserApprovalResponseEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public Guid VocationRequestId { get; set; }
    public VocationRequestEntity VocationRequest { get; set; } = null!;
    public string ApprovalStatus { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
