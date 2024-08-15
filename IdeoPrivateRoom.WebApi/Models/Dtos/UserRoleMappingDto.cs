using IdeoPrivateRoom.WebApi.Data.Entities;

namespace IdeoPrivateRoom.WebApi.Models.Dtos;

public class UserRoleMappingDto
{
    public Guid Id { get; set; }
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
