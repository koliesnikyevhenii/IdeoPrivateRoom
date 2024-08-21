using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Data;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.DAL.Repositories;

public class VocationRepository(ApplicationDbContext _dbContext) : IVocationRepository
{
    public async Task<Guid> Create(VocationRequestEntity vocationRequest)
    {
        var createdVocation = await _dbContext.VocationRequests
            .AddAsync(vocationRequest);

        await _dbContext.SaveChangesAsync();

        return createdVocation.Entity.Id;
    }

    public async Task<List<VocationRequestEntity>> Get()
    {
        return await _dbContext.VocationRequests
            .AsNoTracking()
            .Include(v => v.User)
            .Include(v => v.UserApprovalResponses)
                .ThenInclude(u => u.User)
            .ToListAsync();
    }

    public async Task<List<VocationRequestEntity>> Get(Guid userId)
    {
        return await _dbContext.VocationRequests
            .AsNoTracking()
            .Where(v => v.UserId == userId)
            .Include(v => v.User)
            .Include(v => v.UserApprovalResponses)
                .ThenInclude(u => u.User)
            .ToListAsync();
    }

    public async Task<Guid?> Update(Guid id, VocationRequestEntity vocation)
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
