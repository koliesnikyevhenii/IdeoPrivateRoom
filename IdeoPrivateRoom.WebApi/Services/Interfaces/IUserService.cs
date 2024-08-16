using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Models.Responses;

namespace IdeoPrivateRoom.WebApi.Services.Interfaces;
public interface IUserService
{
    Task<List<UserResponse>> GetAll();
    Task<UserResponse> GetById(Guid id);
    Task<Guid> Create(UserDto user);
    Task<Guid?> Update(Guid id, UserDto user);
    Task<Guid?> Delete(Guid id);
}