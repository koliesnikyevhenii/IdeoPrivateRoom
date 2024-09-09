using AutoMapper;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Models;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Models;
using IdeoPrivateRoom.WebApi.Models.Enums;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
using IdeoPrivateRoom.WebApi.Configurations;
using IdeoPrivateRoom.WebApi.Services.Interfaces;
using LightResults;
using Microsoft.Extensions.Options;
using System;

namespace IdeoPrivateRoom.WebApi.Services;

public class VacationService(
    IVacationRepository _vacationRepository, 
    IMapper _mapper,
    IOptions<VacationsListSettings> settings,
    ILogger<VacationService> _logger) : IVacationService
{
    public async Task<Result<PagedList<VacationResponse>>> GetAll(VacationQueryFilters filters)
    {
        var page = filters.Page ?? 1;
        var pageSize = filters.PageSize ?? settings.Value.PageSize;

        DateTimeOffset? start = DateTimeOffset.TryParse(filters.StartDate, out var parsedStart) ? parsedStart : null;
        DateTimeOffset? end = DateTimeOffset.TryParse(filters.EndDate, out var parsedEnd) ? parsedEnd : null;

        var ids = filters.UserIds?.Split(',')
            .Select(i => Guid.TryParse(i, out var id) ? id : Guid.Empty)
            .Where(i => i != Guid.Empty)
            .ToArray();

        var statuses = filters.Statuses?
            .Split(",")
            .Select(s => s.Trim())
            .ToArray();

        var vacations = await _vacationRepository
            .Get(page, pageSize, start, end, ids, statuses);

        if (vacations.TotalRecords == 0)
        {
            return Result.Fail<PagedList<VacationResponse>>("No vacations was found. Please check your filter criteria.");
        }

        return Result.Ok(_mapper.Map<PagedList<VacationResponse>>(vacations));
    }

    public async Task<Result<Guid>> Create(CreateVacationRequest vacation)
    {
        var newEntity = new VacationRequestEntity
        {
            UserId = Guid.Parse(vacation.UserId),
            StartDate = vacation.StartDate,
            EndDate = vacation.EndDate,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            VacationStatus = ((int)ApprovalStatus.Pending).ToString()
        };
        
        var result = await _vacationRepository.Create(newEntity);

        return Result.Ok(result);
    }

    public async Task<Result<Guid?>> Update(Guid id, UpdateVacationRequest vacation)
    {
        var result = await _vacationRepository.Update(id, _mapper.Map<VacationRequestEntity>(vacation));

        return Result.Ok(result);
    }

    public async Task<Result<Guid?>> Delete(Guid id)
    {
        var result = await _vacationRepository.Delete(id);

        return Result.Ok(result);
    }
}
