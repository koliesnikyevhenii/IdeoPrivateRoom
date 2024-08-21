using AutoMapper;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
using IdeoPrivateRoom.WebApi.Services.Interfaces;

namespace IdeoPrivateRoom.WebApi.Services;

public class UserService(IUserRepository _userRepository, IMapper _mapper) : IUserService
{
    public async Task<List<UserResponse>> GetAll()
    {
        var users = await _userRepository.Get();

        return users.Select(_mapper.Map<UserResponse>).ToList();
    }
    public async Task<UserResponse> GetById(Guid id)
    {
        return _mapper.Map<UserResponse>(await _userRepository.Get(id));
    }


    public async Task<Guid> Create(UserRequest user)
    {
        return await _userRepository.Create(_mapper.Map<UserEntity>(user));
    }

    public async Task<Guid?> Update(Guid id, UserRequest user)
    {
        return await _userRepository.Update(id, _mapper.Map<UserEntity>(user));
    }

    public async Task<Guid?> Delete(Guid id)
    {
        return await _userRepository.Delete(id);
    }
}
