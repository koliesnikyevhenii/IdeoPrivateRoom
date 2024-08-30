using IdeoPrivateRoom.WebApi.Configurations;
using IdeoPrivateRoom.WebApi.Models.Responses;
using IdeoPrivateRoom.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IdeoPrivateRoom.WebApi.Endpoints;

public static class Users
{
    public static void RegisterUserEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/authcheck", [Authorize(Roles = IdeoAppRole.VacationServiceAdminOrUser)] async (IUserService userService, HttpContext context) =>
        {
            return userService.GetAll();
        })
        .WithOpenApi();
     


        var users = routes.MapGroup("/api/users")
            .WithTags("Users");

        users.MapGet("", async (IUserService userService) =>
        {
            var result = await userService.GetAll();
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .Produces<List<UserResponse>>()
        .WithOpenApi();

        users.MapGet("/{id}", async (Guid id, IUserService userService) =>
        {
            var result = await userService.GetById(id);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .Produces<UserResponse>()
        .WithOpenApi();

    }
}
