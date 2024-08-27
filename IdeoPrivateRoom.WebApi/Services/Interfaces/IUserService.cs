using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
using LightResults;

namespace IdeoPrivateRoom.WebApi.Services.Interfaces;
public interface IUserService
{
    Task<Result<List<UserResponse>>> GetAll();
    Task<Result<UserResponse>> GetById(Guid id);
    Task<Result<Guid>> Create(UserRequest user);
    Task<Result<Guid?>> Update(Guid id, UserRequest user);
    Task<Result<Guid?>> Delete(Guid id);
}