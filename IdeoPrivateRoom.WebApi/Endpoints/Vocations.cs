using AutoMapper;
using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Services.Interfaces;

namespace IdeoPrivateRoom.WebApi.Endpoints;

public static class Vocations
{
    public static void RegisterVocationEndpoints(this IEndpointRouteBuilder routes)
    {
        var vocations = routes.MapGroup("/api/v1/vocations")
            .WithTags("Vocations");

        vocations.MapGet("", async (IVocationService vocationService) =>
        {
            return await vocationService.GetAll();
        })
        .WithOpenApi();

        vocations.MapGet("/all", async (IVocationRepository vocationRepository) =>
        {
            return await vocationRepository.GetAllVocations();
        })
        .WithOpenApi();

        vocations.MapGet("/{id}", async (Guid id, IVocationService vocationService) =>
        {
            return await vocationService.GetByUserId(id);
        })
        .WithOpenApi();

        /*vocations.MapPost("", async (VocationRequestDto request, IVocationService vocationService, IMapper mapper) =>
        {
            return await vocationService.Create(request);
        })
        .WithOpenApi();

        vocations.MapPut("/{id}", async (Guid id, VocationRequestDto request, IVocationService vocationService, IMapper mapper) =>
        {
            return await vocationService.Update(id, request);
        })
        .WithOpenApi();

        vocations.MapDelete("/{id}", async (Guid id, IVocationService vocationService) =>
        {
            return await vocationService.Delete(id);
        })
        .WithOpenApi();*/
    }
}
