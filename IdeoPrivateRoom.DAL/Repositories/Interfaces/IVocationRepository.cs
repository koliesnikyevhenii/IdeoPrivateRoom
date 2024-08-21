using IdeoPrivateRoom.DAL.Data.Entities;

namespace IdeoPrivateRoom.DAL.Repositories.Interfaces;
public interface IVocationRepository
{
    Task<List<VocationRequestEntity>> Get();
    Task<List<VocationRequestEntity>> Get(Guid userId);
    Task<Guid> Create(VocationRequestEntity vocationRequest);
    Task<Guid?> Delete(Guid id);
    Task<Guid?> Update(Guid id, VocationRequestEntity vocation);
}
