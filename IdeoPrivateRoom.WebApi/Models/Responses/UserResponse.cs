namespace IdeoPrivateRoom.WebApi.Models.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public List<RoleResponse> Roles { get; set; } = [];
}