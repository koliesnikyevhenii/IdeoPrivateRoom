using IdeoPrivateRoom.WebApi.Models.Dtos;

namespace IdeoPrivateRoom.WebApi.Models.Responses;

public class VocationUserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}
