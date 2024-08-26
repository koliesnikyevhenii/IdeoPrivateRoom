using AutoMapper;
using IdeoPrivateRoom.WebApi.Models;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Services.Interfaces;

namespace IdeoPrivateRoom.WebApi.Endpoints;

public static class Vocations
{
    public static void RegisterVocationEndpoints(this IEndpointRouteBuilder routes)
    {
        var vocations = routes.MapGroup("/api/vocations")
            .WithTags("Vocations");

        vocations.MapGet("", async (
            [AsParameters] VocationQueryFilters filters,
            IVocationService vocationService) =>
        {
            var result = await vocationService.GetAll(filters);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithOpenApi();

        vocations.MapGet("/{id}", async(Guid id, IVocationService vocationService) =>
        {
            var result = await vocationService.GetByUserId(id);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithOpenApi();

        vocations.MapPost("", async (CreateVocationRequest request, IVocationService vocationService, IMapper mapper) =>
        {
            var result = await vocationService.Create(request);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithOpenApi();

        vocations.MapDelete("/{id}", async (Guid id, IVocationService vocationService) =>
        {
            var result = await vocationService.Delete(id);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithOpenApi();
    }
}
