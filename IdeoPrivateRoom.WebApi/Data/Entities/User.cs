namespace IdeoPrivateRoom.WebApi.Data.Entities;

public class User
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
    public ICollection<UserRoleMapping> RoleMappings { get; set; } = [];
    public ICollection<VocationRequest> VocationRequests { get; set; } = [];
    public ICollection<LinkedUser> LinkedUsers { get; set; } = [];
    public ICollection<LinkedUser> AssociatedUsers { get; set; } = [];
    public ICollection<UserApprovalResponse> UserApprovalResponses { get; set; } = [];
}
