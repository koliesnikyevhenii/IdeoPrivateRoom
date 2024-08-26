using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Data;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using IdeoPrivateRoom.DAL.Models;

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

    public async Task<PagedList<VocationRequestEntity>> Get(int page, int pageSize, DateTimeOffset? startDate, DateTimeOffset? endDate, Guid[]? userIds, string[]? statuses)
    {
        var vocations = _dbContext.VocationRequests
            .AsNoTracking()
            .Include(v => v.User)
            .Include(v => v.UserApprovalResponses)
                .ThenInclude(u => u.User)
            .AsQueryable();

        if (userIds != null && userIds.Length != 0)
        {
            vocations = vocations.Where(v => userIds.Contains(v.UserId));
        }
        if (statuses != null && statuses.Length != 0)
        {
            vocations = vocations.Where(v => statuses.Contains(v.VocationStatus));
        }
        if (startDate != null)
        {
            vocations = vocations.Where(v => v.StartDate >= startDate || v.EndDate >= startDate);
        }
        if (startDate != null && endDate != null)
        {
            vocations = vocations.Where(v => v.StartDate >= startDate && v.EndDate <= endDate
                || v.EndDate >= startDate && v.EndDate<= endDate);
        }

        var totalRecords = await vocations.CountAsync();

        var data =  await vocations
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderByDescending(v => v.CreatedDate)
            .ToListAsync();

        return new PagedList<VocationRequestEntity>(data, page, pageSize, totalRecords);
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
