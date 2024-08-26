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

namespace IdeoPrivateRoom.WebApi.Services;

public class VocationService(
    IVocationRepository _vocationRepository, 
    IMapper _mapper,
    IOptions<VocationsListSettings> settings,
    ILogger<VocationService> _logger) : IVocationService
{
    public async Task<Result<PagedList<VocationResponse>>> GetAll(VocationQueryFilters filters)
    {
        var page = filters.Page ?? 1;
        var pageSize = filters.PageSize ?? settings.Value.PageSize;

        DateTimeOffset? start = DateTimeOffset.TryParse(filters.StartDate, out var parsedStart) ? parsedStart : null;
        DateTimeOffset? end = DateTimeOffset.TryParse(filters.EndDate, out var parsedEnd) ? parsedEnd : null;

        var ids = filters.UserIds?.Split(',')
            .Select(i => Guid.TryParse(i, out var id) ? id : Guid.Empty)
            .Where(i => i != Guid.Empty)
            .ToArray();

        var statuses = filters.Statuses?.Split(",");

        var vocations = await _vocationRepository
            .Get(page, pageSize, start, end, ids, statuses);

        if (vocations.TotalRecords == 0)
        {
            return Result.Fail<PagedList<VocationResponse>>("No vocations was found.");
        }

        return Result.Ok(_mapper.Map<PagedList<VocationResponse>>(vocations));
    }
    public async Task<Result<List<VocationResponse>>> GetByUserId(Guid id)
    {
        var vocations = await _vocationRepository.Get(id);

        var result = vocations.Select(_mapper.Map<VocationResponse>).ToList();

        return Result.Ok(result);
    }

    public async Task<Result<Guid>> Create(CreateVocationRequest vocation)
    {
        var createdVocation = new VocationRequestEntity
        {
            UserId = vocation.UserId,
            StartDate = vocation.StartDate,
            EndDate = vocation.EndDate,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            VocationStatus = ((int)ApprovalStatus.Approved).ToString()
        };
        
        var result = await _vocationRepository.Create(createdVocation);

        return Result.Ok(result);
    }

    public async Task<Result<Guid?>> Update(Guid id, VocationRequestEntity vocation)
    {
        var result = await _vocationRepository.Update(id, _mapper.Map<VocationRequestEntity>(vocation));

        return Result.Ok(result);
    }

    public async Task<Result<Guid?>> Delete(Guid id)
    {
        var result = await _vocationRepository.Delete(id);

        return Result.Ok(result);
    }
}
