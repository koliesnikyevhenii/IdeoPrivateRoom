using AutoMapper;
using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Services.Interfaces;

namespace IdeoPrivateRoom.WebApi.Endpoints;

public static class Users
{
    public static void RegisterUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var users = routes.MapGroup("/api/v1/users")
            .WithTags("Users");

        users.MapGet("", async (IUserService userService) =>
        {
            return await userService.GetAll();
        })
        .WithOpenApi();

        users.MapGet("/{id}", async (Guid id, IUserService userService) =>
        {
            return await userService.GetById(id);
        })
        .WithOpenApi();

        users.MapGet("/all", async (IUserRepository userRepository) =>
        {
            return await userRepository.GetAll();
        });

        /*users.MapPost("", async (UserRequest request, IUserService userService, IMapper mapper) =>
        {
            return await userService.Create(mapper.Map<UserDto>(request));
        })
        .WithOpenApi();

        users.MapPut("/{id}", async (Guid id, UserRequest request, IUserService userService, IMapper mapper) =>
        {
            var user = mapper.Map<UserDto>(request);
            return await userService.Update(id, user);
        })
        .WithOpenApi();

        users.MapDelete("/{id}", async (Guid id, IUserService userService) =>
        {
            return await userService.Delete(id);
        })
        .WithOpenApi();*/
    }
}
