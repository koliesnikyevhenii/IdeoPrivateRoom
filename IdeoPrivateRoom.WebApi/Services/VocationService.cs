using AutoMapper;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Models.Enums;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
using IdeoPrivateRoom.WebApi.Services.Interfaces;

namespace IdeoPrivateRoom.WebApi.Services;

public class VocationService(
    IVocationRepository _vocationRepository, 
    IMapper _mapper,
    ILogger<VocationService> _logger) : IVocationService
{
    public async Task<List<VocationResponse>> GetAll(string? userIds, string? statuses, string? startDate, string? endDate)
    {
        DateTimeOffset? start = DateTimeOffset.TryParse(startDate, out var parsedStart) ? parsedStart : null;
        DateTimeOffset? end = DateTimeOffset.TryParse(endDate, out var parsedEnd) ? parsedEnd : null;

        var ids = userIds?.Split(',')
            .Select(i => Guid.TryParse(i, out var id) ? id : Guid.Empty)
            .Where(i => i != Guid.Empty)
            .ToList();
        
        var vocations = await _vocationRepository
            .Get(start, end, ids, statuses);

        return vocations.OrderByDescending(v => v.CreatedDate)
            .Select(_mapper.Map<VocationResponse>)
            .ToList();
    }
    public async Task<List<VocationResponse>> GetByUserId(Guid id)
    {
        var vocations = await _vocationRepository.Get(id);

        return vocations.Select(_mapper.Map<VocationResponse>).ToList();
    }

    public async Task<Guid> Create(CreateVocationRequest vocation)
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
        return await _vocationRepository.Create(createdVocation);
    }

    public async Task<Guid?> Update(Guid id, VocationRequestEntity vocation)
    {
        return await _vocationRepository.Update(id, _mapper.Map<VocationRequestEntity>(vocation));
    }

    public async Task<Guid?> Delete(Guid id)
    {
        return await _vocationRepository.Delete(id);
    }
}
