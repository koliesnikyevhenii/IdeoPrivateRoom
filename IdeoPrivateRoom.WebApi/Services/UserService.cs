using AutoMapper;
using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Models.Responses;
using IdeoPrivateRoom.WebApi.Repositories.Interfaces;
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


    public async Task<Guid> Create(UserDto user)
    {
        return await _userRepository.Create(user);
    }

    public async Task<Guid?> Update(Guid id, UserDto user)
    {
        return await _userRepository.Update(id, user);
    }

    public async Task<Guid?> Delete(Guid id)
    {
        return await _userRepository.Delete(id);
    }
}
