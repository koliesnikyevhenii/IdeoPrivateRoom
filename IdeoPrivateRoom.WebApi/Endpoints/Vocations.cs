using IdeoPrivateRoom.WebApi.Services.Interfaces;

namespace IdeoPrivateRoom.WebApi.Endpoints;

public static class Vocations
{
    public static void RegisterVocationEndpoints(this IEndpointRouteBuilder routes)
    {
        var vocations = routes.MapGroup("/api/v1/vocations")
            .WithTags("Vocations");

        vocations.MapGet("", (IVocationService vocationService) =>
        {
            return vocationService.GetAll();
        })
        .WithOpenApi();

        vocations.MapGet("/{id}", (Guid id, IVocationService vocationService) =>
        {
            return vocationService.GetByUserId(id);
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
