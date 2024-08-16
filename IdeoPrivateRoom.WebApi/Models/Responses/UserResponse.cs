namespace IdeoPrivateRoom.WebApi.Models.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserIcon { get; set; } = string.Empty;
    public List<RoleResponse> Roles { get; set; } = [];
}