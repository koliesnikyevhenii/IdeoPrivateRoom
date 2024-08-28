using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Data;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using IdeoPrivateRoom.DAL.Models;

namespace IdeoPrivateRoom.DAL.Repositories;

public class VacationRepository(ApplicationDbContext _dbContext) : IVacationRepository
{
    public async Task<Guid> Create(VacationRequestEntity vacationRequest)
    {
        var createdVacation = await _dbContext.VacationRequests
            .AddAsync(vacationRequest);

        await _dbContext.SaveChangesAsync();

        return createdVacation.Entity.Id;
    }

    public async Task<PagedList<VacationRequestEntity>> Get(int page, int pageSize, DateTimeOffset? startDate, DateTimeOffset? endDate, Guid[]? userIds, string[]? statuses)
    {
        var vacations = _dbContext.VacationRequests
            .AsNoTracking()
            .Include(v => v.User)
                .ThenInclude(u => u.LinkedUsers)
                    .ThenInclude(lu => lu.AssociatedUser)
                        .ThenInclude(au => au.UserApprovalResponses)
            .AsQueryable();

        if (userIds != null && userIds.Length != 0)
        {
            vacations = vacations.Where(v => userIds.Contains(v.UserId));
        }
        if (statuses != null && statuses.Length != 0)
        {
            vacations = vacations.Where(v => statuses.Contains(v.VacationStatus));
        }
        if (startDate != null)
        {
            vacations = vacations.Where(v => v.StartDate >= startDate || v.EndDate >= startDate);
        }
        if (startDate != null && endDate != null)
        {
            vacations = vacations.Where(v => v.StartDate >= startDate && v.EndDate <= endDate
                || v.EndDate >= startDate && v.EndDate<= endDate);
        }

        var totalRecords = await vacations.CountAsync();

        var data =  await vacations
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderByDescending(v => v.CreatedDate)
            .ToListAsync();

        return new PagedList<VacationRequestEntity>(data, page, pageSize, totalRecords);
    }

    public async Task<Guid?> Update(Guid id, VacationRequestEntity vacation)
    {
        await _dbContext.VacationRequests
            .Where(v => v.Id == id)
            .ExecuteUpdateAsync(v => v
                .SetProperty(p => p.StartDate, p => vacation.StartDate)
                .SetProperty(p => p.EndDate, p => vacation.EndDate)
                .SetProperty(p => p.Comment, p => vacation.Comment)
                .SetProperty(p => p.UpdatedDate, p => vacation.UpdatedDate)
                .SetProperty(p => p.VacationStatus, p => vacation.VacationStatus));

        return id;
    }

    public async Task<Guid?> Delete(Guid id)
    {
        await _dbContext.VacationRequests
            .Where(v => v.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
