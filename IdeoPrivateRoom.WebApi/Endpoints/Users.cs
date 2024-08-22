using IdeoPrivateRoom.WebApi.Services.Interfaces;

namespace IdeoPrivateRoom.WebApi.Endpoints;

public static class Users
{
    public static void RegisterUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var users = routes.MapGroup("/api/users")
            .WithTags("Users");

        users.MapGet("", async (IUserService userService) =>
        {
            var result = await userService.GetAll();
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result.Error.Message);
        })
        .WithOpenApi();

        users.MapGet("/{id}", async (Guid id, IUserService userService) =>
        {
            var result = await userService.GetById(id);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result.Error.Message);
        })
        .WithOpenApi();

        /*users.MapGet("/all", (IUserRepository userRepository) =>
        {
            return userRepository.GetAll();
        });*/

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
