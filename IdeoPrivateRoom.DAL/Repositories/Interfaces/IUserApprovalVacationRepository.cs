using IdeoPrivateRoom.DAL.Data.Entities;

namespace IdeoPrivateRoom.DAL.Repositories.Interfaces;
public interface IUserApprovalVacationRepository
{
    Task<Guid> Create(UserApprovalResponseEntity userApprovalResponse);
    Task<UserApprovalResponseEntity?> GetByUserIdAndVacationId(Guid userId, Guid vacationId);
    Task<List<UserApprovalResponseEntity>> GetByVacationId(Guid vacationId);
    Task<Guid> Update(UserApprovalResponseEntity userApprovalResponse);
}