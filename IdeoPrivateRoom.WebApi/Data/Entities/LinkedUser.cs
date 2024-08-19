namespace IdeoPrivateRoom.WebApi.Data.Entities;

public class LinkedUser
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid LinkedUserId { get; set; }
    public User AssociatedUser { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
