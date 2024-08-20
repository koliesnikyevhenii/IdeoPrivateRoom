using IdeoPrivateRoom.WebApi.Data.Entities;

namespace IdeoPrivateRoom.WebApi.Models.Dtos;

public class UserRoleMappingDto
{
    public Guid Id { get; set; }
    public UserEntity User { get; set; } = null!;
    public RoleEntity Role { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
