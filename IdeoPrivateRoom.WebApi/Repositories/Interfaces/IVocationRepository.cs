using IdeoPrivateRoom.WebApi.Models.Dtos;

namespace IdeoPrivateRoom.WebApi.Repositories.Interfaces;
public interface IVocationRepository
{
    Task<List<VocationRequestDto>> Get();
    Task<List<VocationRequestDtoVTest>> GetAllVocations();
    Task<List<VocationRequestDto>> Get(Guid userId);
    Task<Guid> Create(VocationRequestDto vocationRequest);
    Task<Guid?> Delete(Guid id);
    Task<Guid?> Update(Guid id, VocationRequestDto vocation);
}