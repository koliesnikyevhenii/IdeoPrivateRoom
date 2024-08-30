using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Models;

namespace IdeoPrivateRoom.DAL.Repositories.Interfaces;
public interface IVacationRepository
{
    Task<VacationRequestEntity?> Get(Guid vacationId);
    Task<PagedList<VacationRequestEntity>> Get(int page, int pageSize, DateTimeOffset? startDate, DateTimeOffset? endDate, Guid[]? userIds, string[]? statuses);
    Task<List<LinkedUserApprovalStatusDto>> GetVacationApprovaStatuses(Guid vacationId);
    Task<Guid> Create(VacationRequestEntity vacationRequest);
    Task<Guid?> Delete(Guid id);
    Task<Guid?> Update(Guid id, VacationRequestEntity vacation);
}
