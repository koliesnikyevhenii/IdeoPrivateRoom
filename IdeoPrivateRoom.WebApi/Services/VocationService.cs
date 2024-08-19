using IdeoPrivateRoom.WebApi.Endpoints;
using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Models.Responses;
using IdeoPrivateRoom.WebApi.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Services.Interfaces;

namespace IdeoPrivateRoom.WebApi.Services;

public class VocationService(IVocationRepository _vocationRepository) : IVocationService
{
    public async Task<List<VocationResponse>> GetAll()
    {
        var vocations = await _vocationRepository.Get();

        return vocations.Select(v => new VocationResponse
        {
            Id = v.Id,
            Title = v.Title,
            StartDate = v.StartDate,
            EndDate = v.EndDate,
            VocationStatus = v.VocationStatus
        }).ToList();
    }
    public async Task<List<VocationResponse>> GetByUserId(Guid id)
    {
        var vocations = await _vocationRepository.Get(id);

        return vocations.Select(v => new VocationResponse
        {
            Id = v.Id,
            Title = v.Title,
            StartDate = v.StartDate,
            EndDate = v.EndDate,
            VocationStatus = v.VocationStatus
        }).ToList();
    }

    public async Task<Guid> Create(VocationRequestDto vocation)
    {
        return await _vocationRepository.Create(vocation);
    }

    public async Task<Guid?> Update(Guid id, VocationRequestDto vocation)
    {
        return await _vocationRepository.Update(id, vocation);
    }

    public async Task<Guid?> Delete(Guid id)
    {
        return await _vocationRepository.Delete(id);
    }
}
