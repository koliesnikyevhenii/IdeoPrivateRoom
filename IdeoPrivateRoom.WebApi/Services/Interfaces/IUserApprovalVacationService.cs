using IdeoPrivateRoom.WebApi.Models.Requests;
using LightResults;

namespace IdeoPrivateRoom.WebApi.Services.Interfaces;
public interface IUserApprovalVacationService
{
    Task<Result<Guid>> Update(UpdateApprovalStatusRequest request);
}