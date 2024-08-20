namespace IdeoPrivateRoom.WebApi.Models;

public class LInkedUser
{
    public Guid Id { get; set; }
    public User User { get; set; } = null!;
    public Guid LinkedUserId { get; set; }
    public User AssociatedUser { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
