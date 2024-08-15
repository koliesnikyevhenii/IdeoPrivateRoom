using IdeoPrivateRoom.WebApi.Data.Entities;

namespace IdeoPrivateRoom.WebApi.Models.Dtos;

public class VocationDto
{
    public Guid Id { get; set; }
    public User User { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
