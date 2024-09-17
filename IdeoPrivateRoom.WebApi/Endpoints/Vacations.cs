using AutoMapper;
using IdeoPrivateRoom.DAL.Models;
using IdeoPrivateRoom.WebApi.Extension;
using IdeoPrivateRoom.WebApi.Models;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
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
        .Produces<PagedList<VacationResponse>>()
        .WithOpenApi();

        vacations.MapPost("", async (CreateVacationRequest request, IVacationService vacationService, IMapper mapper) =>
        {
            var result = await vacationService.Create(request);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithRequestValidation<CreateVacationRequest>()
        .Produces<Guid>()
        .WithOpenApi();

        vacations.MapPost("/status", async (UpdateApprovalStatusRequest request, IUserApprovalVacationService approvalService) =>
        {
            var result = await approvalService.Update(request);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithRequestValidation<UpdateApprovalStatusRequest>()
        .Produces<Guid>()
        .WithOpenApi();

        vacations.MapPut("/{id}", async (Guid id, UpdateVacationRequest request, IVacationService vacationService) =>
        {
            var result = await vacationService.Update(id, request);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .WithRequestValidation<UpdateVacationRequest>()
        .Produces<Guid>()
        .WithOpenApi();

        vacations.MapDelete("/{id}", async (Guid id, IVacationService vacationService) =>
        {
            var result = await vacationService.Delete(id);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error.Message);
        })
        .Produces<Guid>()
        .WithOpenApi();
    }
}
