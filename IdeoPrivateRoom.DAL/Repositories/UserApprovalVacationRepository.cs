using IdeoPrivateRoom.DAL.Data;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.DAL.Repositories;
public class UserApprovalVacationRepository(ApplicationDbContext _dbContext) : IUserApprovalVacationRepository
{
    public async Task<UserApprovalResponseEntity?> GetByUserIdAndVacationId(Guid userId, Guid vacationId)
    {
        return await _dbContext.UserApprovalResponses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.VacationRequestId == vacationId);
    }

    public async Task<List<UserApprovalResponseEntity>> GetByVacationId(Guid vacationId)
    {
        return await _dbContext.UserApprovalResponses
            .AsNoTracking()
            .Where(x => x.VacationRequestId == vacationId)
            .ToListAsync();
    }

    public async Task<Guid> Create(UserApprovalResponseEntity userApprovalResponse)
    {
        var created = await _dbContext.UserApprovalResponses
            .AddAsync(userApprovalResponse);

        await _dbContext.SaveChangesAsync();

        return created.Entity.Id;
    }
    public async Task<Guid> Update(UserApprovalResponseEntity userApprovalResponse)
    {
        var updated = await _dbContext.UserApprovalResponses
            .Where(x => x.Id == userApprovalResponse.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.ApprovalStatus, p => userApprovalResponse.ApprovalStatus)
                .SetProperty(p => p.UpdatedDate, p => userApprovalResponse.UpdatedDate));

        await _dbContext.SaveChangesAsync();

        return userApprovalResponse.Id;
    }
}
