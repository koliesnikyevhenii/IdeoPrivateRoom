using IdeoPrivateRoom.DAL.Data.Entities;

namespace IdeoPrivateRoom.DAL.Repositories.Interfaces;
public interface IUserRepository
{
    Task<List<UserEntity>> Get();
    Task<UserEntity?> Get(Guid id);
    Task<Guid> Create(UserEntity user);
    Task<Guid?> Update(Guid id, UserEntity user);
    Task<Guid?> Delete(Guid id);
}
