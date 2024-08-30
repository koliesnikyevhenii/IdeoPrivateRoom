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

        var approvalResponses = await _vacationRepository.GetVacationApprovaStatuses(vacationId);

        var statuses = approvalResponses.Select(r => r.ApprovalStatus.ToEnum(ApprovalStatus.Pending)).ToList();

        var vacationStatus = ((int)CalculateGeneralStatus(statuses)).ToString();

        var vacation = await _vacationRepository.Get(vacationId);

        vacation!.VacationStatus = vacationStatus;

        await _vacationRepository.Update(vacationId, vacation);

        return Result.Ok(vacationId);
    }

    private static ApprovalStatus CalculateGeneralStatus(List<ApprovalStatus> statuses)
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
