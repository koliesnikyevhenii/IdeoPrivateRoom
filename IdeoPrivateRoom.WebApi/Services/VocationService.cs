using AutoMapper;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Models.Enums;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
using IdeoPrivateRoom.WebApi.Services.Interfaces;
using LightResults;

namespace IdeoPrivateRoom.WebApi.Services;

public class VocationService(
    IVocationRepository _vocationRepository, 
    IMapper _mapper,
    ILogger<VocationService> _logger) : IVocationService
{
    public async Task<Result<List<VocationResponse>>> GetAll(VocationQueryFilters filters)
    {
        DateTimeOffset? start = DateTimeOffset.TryParse(filters.StartDate, out var parsedStart) ? parsedStart : null;
        DateTimeOffset? end = DateTimeOffset.TryParse(filters.EndDate, out var parsedEnd) ? parsedEnd : null;

        var ids = filters.UserIds?.Split(',')
            .Select(i => Guid.TryParse(i, out var id) ? id : Guid.Empty)
            .Where(i => i != Guid.Empty)
            .ToList();
        
        var vocations = await _vocationRepository
            .Get(start, end, ids, filters.Statuses);

        if (vocations?.Any() != true)
        {
            return Result.Fail<List<VocationResponse>>("No vocations found matching the given criteria.");
        }

        var result = vocations.OrderByDescending(v => v.CreatedDate)
            .Select(_mapper.Map<VocationResponse>)
            .ToList();

        return Result.Ok(result);

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
