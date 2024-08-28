using AutoMapper;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
using IdeoPrivateRoom.WebApi.Services.Interfaces;
using LightResults;

namespace IdeoPrivateRoom.WebApi.Services;

public class UserService(IUserRepository _userRepository, IMapper _mapper) : IUserService
{
    public async Task<Result<List<UserResponse>>> GetAll()
    {
        var users = await _userRepository.Get();

        if (users?.Any() != true)
        {
            return Result.Fail<List<UserResponse>>("No users found");
        }

        var mappedUsers = users.Select(_mapper.Map<UserResponse>).ToList();

        return Result.Ok(mappedUsers);
    }
    public async Task<Result<UserResponse>> GetById(Guid id)
    {
        var user = await _userRepository.Get(id);

        return user != null
            ? Result.Ok(_mapper.Map<UserResponse>(user))
            : Result.Fail<UserResponse>("User not found");
    }


    public async Task<Result<Guid>> Create(UserRequest user)
    {
        var result = await _userRepository.Create(_mapper.Map<UserEntity>(user));

        return Result.Ok(result);
    }

    public async Task<Result<Guid?>> Update(Guid id, UserRequest user)
    {
        var result = await _userRepository.Update(id, _mapper.Map<UserEntity>(user));

        return Result.Ok(result);
    }

    public async Task<Result<Guid?>> Delete(Guid id)
    {
        var result = await _userRepository.Delete(id);

        return Result.Ok(result);
    }
}
