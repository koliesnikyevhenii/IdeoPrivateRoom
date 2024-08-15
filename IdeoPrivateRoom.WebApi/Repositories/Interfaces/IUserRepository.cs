using IdeoPrivateRoom.WebApi.Data.Entities;

namespace IdeoPrivateRoom.WebApi.Repositories.Interfaces;

public interface IUserRepository
{
    Task<Guid> Add(User user);
    Task<List<User>> Get();
}