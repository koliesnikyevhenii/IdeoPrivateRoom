namespace IdeoPrivateRoom.WebApi.Models;

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
    public IEnumerable<Role> Roles { get; set; } = [];
}
