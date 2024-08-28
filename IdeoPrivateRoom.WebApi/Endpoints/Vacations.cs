using AutoMapper;
using IdeoPrivateRoom.WebApi.Models;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Services.Interfaces;

namespace IdeoPrivateRoom.WebApi.Endpoints;

public static class Vacations
{
    public static void RegisterVacationEndpoints(this IEndpointRouteBuilder routes)
    {
        var vacations = routes.MapGroup("/api/vacations")
            .WithTags("Vacations");

        vacations.MapGet("", async (
            [AsParameters] VacationQueryFilters filters,
            IVacationService vacationService) =>
        {
            var result = await vacationService.GetAll(filters);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithOpenApi();

        vacations.MapGet("/{id}", async(Guid id, IVacationService vacationService) =>
        {
            var result = await vacationService.GetByUserId(id);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithOpenApi();

        vacations.MapPost("", async (CreateVacationRequest request, IVacationService vacationService, IMapper mapper) =>
        {
            var result = await vacationService.Create(request);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithOpenApi();

        vacations.MapDelete("/{id}", async (Guid id, IVacationService vacationService) =>
        {
            var result = await vacationService.Delete(id);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithOpenApi();
    }
}
