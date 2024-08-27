using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Models;
using IdeoPrivateRoom.WebApi.Models;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
using LightResults;

namespace IdeoPrivateRoom.WebApi.Services.Interfaces;
public interface IVocationService
{
    Task<Result<PagedList<VocationResponse>>> GetAll(VocationQueryFilters filters);
    Task<Result<Guid>> Create(CreateVocationRequest vocation);
    Task<Result<Guid?>> Delete(Guid id);
    Task<Result<Guid?>> Update(Guid id, VocationRequestEntity vocation);
}