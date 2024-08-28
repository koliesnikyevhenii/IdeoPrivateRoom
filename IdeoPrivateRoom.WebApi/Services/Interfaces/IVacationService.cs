using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Models;
using IdeoPrivateRoom.WebApi.Models;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
using LightResults;

namespace IdeoPrivateRoom.WebApi.Services.Interfaces;
public interface IVacationService
{
    Task<Result<PagedList<VacationResponse>>> GetAll(VacationQueryFilters filters);
    Task<Result<List<VacationResponse>>> GetByUserId(Guid id);
    Task<Result<Guid>> Create(CreateVacationRequest vacation);
    Task<Result<Guid?>> Delete(Guid id);
    Task<Result<Guid?>> Update(Guid id, VacationRequestEntity vacation);
}