using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;

namespace IdeoPrivateRoom.WebApi.Services.Interfaces;
public interface IUserService
{
    Task<List<UserResponse>> GetAll();
    Task<UserResponse> GetById(Guid id);
    Task<Guid> Create(UserRequest user);
    Task<Guid?> Update(Guid id, UserRequest user);
    Task<Guid?> Delete(Guid id);
}