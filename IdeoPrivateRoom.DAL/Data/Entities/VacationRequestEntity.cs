namespace IdeoPrivateRoom.DAL.Data.Entities;

public class VacationRequestEntity
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string VacationStatus { get; set; } = string.Empty;

    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public ICollection<UserApprovalResponseEntity> UserApprovalResponses { get; set; } = [];

}
