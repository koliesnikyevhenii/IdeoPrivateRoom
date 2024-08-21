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

        vocations.MapGet("", (
            [AsParameters] VocationQueryFilters filters,
            IVocationService vocationService) =>
        {
            return vocationService.GetAll(filters.UserIds, filters.Statuses, filters.StartDate, filters.EndDate);
        })
        .WithOpenApi();

        vocations.MapGet("/{id}", (Guid id, IVocationService vocationService) =>
        {
            return vocationService.GetByUserId(id);
        })
        .WithOpenApi();

        vocations.MapPost("", async (CreateVocationRequest request, IVocationService vocationService, IMapper mapper) =>
        {
            return await vocationService.Create(request);
        })
        .WithOpenApi();

        vocations.MapDelete("/{id}", async (Guid id, IVocationService vocationService) =>
        {
            return await vocationService.Delete(id);
        })
        .WithOpenApi();
    }
}
