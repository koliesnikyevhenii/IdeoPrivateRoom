using AutoMapper;
using IdeoPrivateRoom.WebApi.Data;
using IdeoPrivateRoom.WebApi.Data.Entities;
using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.WebApi.Repositories;

public class VocationRepository(ApplicationDbContext _dbContext, IMapper _mapper) : IVocationRepository
{
    public async Task<Guid> Create(VocationRequestEntity vocationRequest)
    {
        var createdVocation = await _dbContext.VocationRequests
            .AddAsync(vocationRequest);

        await _dbContext.SaveChangesAsync();

        return createdVocation.Entity.Id;
    }

    public async Task<List<VocationRequestDto>> Get()
    {
        return await _dbContext.VocationRequests
            .AsNoTracking()
            .Include(v => v.User)
            .Include(v => v.UserApprovalResponses)
                .ThenInclude(u => u.User)
            .Select(v => _mapper.Map<VocationRequestDto>(v))
            .ToListAsync();
    }

    public async Task<List<VocationRequestDto>> Get(Guid userId)
    {
        return await _dbContext.VocationRequests
            .AsNoTracking()
            .Where(v => v.UserId == userId)
            .Select(v => _mapper.Map<VocationRequestDto>(v))
            .ToListAsync();
    }

    public async Task<Guid?> Update(Guid id, VocationRequestDto vocation)
    {
        await _dbContext.VocationRequests
            .Where(v => v.Id == id)
            .ExecuteUpdateAsync(v => v
                .SetProperty(p => p.StartDate, p => vocation.StartDate)
                .SetProperty(p => p.EndDate, p => vocation.EndDate)
                .SetProperty(p => p.UpdatedDate, p => vocation.UpdatedDate)
                .SetProperty(p => p.CreatedDate, p => vocation.CreatedDate)
                .SetProperty(p => p.VocationStatus, p => vocation.VocationStatus));

        return id;
    }

    public async Task<Guid?> Delete(Guid id)
    {
        await _dbContext.VocationRequests
            .Where(v => v.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
