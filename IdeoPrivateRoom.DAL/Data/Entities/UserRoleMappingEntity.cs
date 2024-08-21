namespace IdeoPrivateRoom.DAL.Data.Entities;

public class UserRoleMappingEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public Guid RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
