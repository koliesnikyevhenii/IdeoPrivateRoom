using FluentValidation;
using IdeoPrivateRoom.WebApi.Models.Requests;

namespace IdeoPrivateRoom.WebApi.Models.Validators;

public class UpdateApprovalStatusValidator : AbstractValidator<UpdateApprovalStatusRequest>
{
    public UpdateApprovalStatusValidator()
    {
        RuleFor(x => x.VacationId).Must(x => Guid.TryParse(x, out var _)).WithMessage("Please specify a valid Vacation Id.");
        RuleFor(x => x.UserId).Must(x => Guid.TryParse(x, out var _)).WithMessage("Please specify a valid User Id.");
        RuleFor(x => x.Status).Must(value => value == "0" || value == "1" || value == "2")
            .WithMessage("ApprovalStatus must be '0', '1', or '2'.");
    }
}
