namespace IdeoPrivateRoom.WebApi.Data.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserIcon { get; set; } = string.Empty;
    public DateTime UpdatedDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }
    public ICollection<UserRoleMappingEntity> RoleMappings { get; set; } = [];
    public ICollection<VocationRequestEntity> VocationRequests { get; set; } = [];
    public ICollection<LinkedUserEntity> LinkedUsers { get; set; } = [];
    public ICollection<LinkedUserEntity> AssociatedUsers { get; set; } = [];
    public ICollection<UserApprovalResponseEntity> UserApprovalResponses { get; set; } = [];
}
