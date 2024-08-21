using AutoMapper;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Models.Enums;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
using IdeoPrivateRoom.WebApi.Services.Interfaces;

namespace IdeoPrivateRoom.WebApi.Services;

public class VocationService(IVocationRepository _vocationRepository, IMapper _mapper) : IVocationService
{
    public async Task<List<VocationResponse>> GetAll()
    {
        var vocations = await _vocationRepository.Get();

        return vocations.Select(_mapper.Map<VocationResponse>).ToList();
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
