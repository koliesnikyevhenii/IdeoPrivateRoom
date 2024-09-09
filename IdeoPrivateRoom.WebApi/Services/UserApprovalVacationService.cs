using AutoMapper;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using IdeoPrivateRoom.WebApi.Extension;
using IdeoPrivateRoom.WebApi.Models.Enums;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Services.Interfaces;
using LightResults;

namespace IdeoPrivateRoom.WebApi.Services;

public class UserApprovalVacationService(
    IUserApprovalVacationRepository _userApprovalVacationRepository,
    IVacationRepository _vacationRepository) : IUserApprovalVacationService
{
    public async Task<Result<Guid>> Update(UpdateApprovalStatusRequest request)
    {
        var userId = Guid.Parse(request.UserId);
        var vacationId = Guid.Parse(request.VacationId);

        await CreateOrUpdateApprovalResponse(request, userId, vacationId);

        await CalculateAndUpdateVacationStatus(vacationId);

        return Result.Ok(vacationId);
    }

    private async Task CalculateAndUpdateVacationStatus(Guid vacationId)
    {
        var statuses = (await _vacationRepository.GetVacationApprovaStatuses(vacationId))
                    .Select(s => s.ToEnum(ApprovalStatus.Pending))
                    .ToList();

        var vacationStatus = ((int)(object)CalculateVacationStatus(statuses)).ToString();

        var vacation = await _vacationRepository.Get(vacationId);

        vacation!.VacationStatus = vacationStatus;

        await _vacationRepository.Update(vacationId, vacation);
    }

    private async Task CreateOrUpdateApprovalResponse(UpdateApprovalStatusRequest request, Guid userId, Guid vacationId)
    {
        var approvalResponse = await _userApprovalVacationRepository.GetByUserIdAndVacationId(userId, vacationId);

        if (approvalResponse == null)
        {
            approvalResponse = new UserApprovalResponseEntity
            {
                UserId = userId,
                VacationRequestId = vacationId,
                ApprovalStatus = request.Status,
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };
            await _userApprovalVacationRepository.Create(approvalResponse);
        }
        else
        {
            approvalResponse.ApprovalStatus = request.Status;
            await _userApprovalVacationRepository.Update(approvalResponse);
        }
    }

    private static ApprovalStatus CalculateVacationStatus(List<ApprovalStatus> statuses)
    {
        if (statuses.Contains(ApprovalStatus.Rejected))
        {
            return ApprovalStatus.Rejected;
        }

        if (statuses.Contains(ApprovalStatus.Pending))
        {
            return ApprovalStatus.Pending;
        }

        return ApprovalStatus.Approved;
    }
}
