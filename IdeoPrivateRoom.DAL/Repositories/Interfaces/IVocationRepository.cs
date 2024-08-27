using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Models;

namespace IdeoPrivateRoom.DAL.Repositories.Interfaces;
public interface IVocationRepository
{
    Task<PagedList<VocationRequestEntity>> Get(int page, int pageSize, DateTimeOffset? startDate, DateTimeOffset? endDate, Guid[]? userIds, string[]? statuses);
    Task<Guid> Create(VocationRequestEntity vocationRequest);
    Task<Guid?> Delete(Guid id);
    Task<Guid?> Update(Guid id, VocationRequestEntity vocation);
}
