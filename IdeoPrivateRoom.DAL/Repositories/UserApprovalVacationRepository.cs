using IdeoPrivateRoom.DAL.Data;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.DAL.Repositories;
public class UserApprovalVacationRepository(ApplicationDbContext _dbContext) : IUserApprovalVacationRepository
{
    public Task<UserApprovalResponseEntity?> GetByUserIdAndVacationId(Guid userId, Guid vacationId)
    {
        return _dbContext.UserApprovalResponses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.VacationRequestId == vacationId);
    }

    public async Task<Guid> Create(UserApprovalResponseEntity userApprovalResponse)
    {
        var createdEntity = await _dbContext.UserApprovalResponses
            .AddAsync(userApprovalResponse);

        await _dbContext.SaveChangesAsync();

        return createdEntity.Entity.Id;
    }
    public async Task<Guid> Update(UserApprovalResponseEntity userApprovalResponse)
    {
        await _dbContext.UserApprovalResponses
            .Where(x => x.Id == userApprovalResponse.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.ApprovalStatus, p => userApprovalResponse.ApprovalStatus)
                .SetProperty(p => p.UpdatedDate, p => userApprovalResponse.UpdatedDate));
        
        await _dbContext.SaveChangesAsync();

        return userApprovalResponse.Id;
    }
}
