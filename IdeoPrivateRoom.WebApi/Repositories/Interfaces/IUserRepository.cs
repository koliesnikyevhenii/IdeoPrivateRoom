using IdeoPrivateRoom.WebApi.Models.Dtos;
using static IdeoPrivateRoom.WebApi.Repositories.UserRepository;

namespace IdeoPrivateRoom.WebApi.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<UserDto>> Get();
    Task<UserDto> Get(Guid id);
    Task<Guid> Create(UserDto user);
    Task<Guid?> Update(Guid id, UserDto user);
    Task<Guid?> Delete(Guid id);
    Task<List<UserDtoTest>> GetAll();
}