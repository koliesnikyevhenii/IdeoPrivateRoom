using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;
using LightResults;

namespace IdeoPrivateRoom.WebApi.Services.Interfaces;
public interface IVocationService
{
    Task<Result<List<VocationResponse>>> GetAll(string? userIds, string? statuses, string? startDate, string? endDate);
    Task<Result<List<VocationResponse>>> GetByUserId(Guid id);
    Task<Result<Guid>> Create(CreateVocationRequest vocation);
    Task<Result<Guid?>> Delete(Guid id);
    Task<Result<Guid?>> Update(Guid id, VocationRequestEntity vocation);
}