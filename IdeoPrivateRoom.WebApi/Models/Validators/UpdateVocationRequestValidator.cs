using FluentValidation;
using IdeoPrivateRoom.WebApi.Models.Enums;
using IdeoPrivateRoom.WebApi.Models.Requests;

namespace IdeoPrivateRoom.WebApi.Models.Validators;

public class UpdateVocationRequestValidator : AbstractValidator<UpdateVocationRequest>
{
    public UpdateVocationRequestValidator()
    {
        RuleFor(x => x.End).GreaterThan(x => x.Start).When(x => x.End.HasValue && x.Start.HasValue).WithMessage("End date cannot be earlier than the start date. Check start date");
        RuleFor(x => x.Status).Must(s => Enum.TryParse(s, true, out ApprovalStatus _)).When(s => !string.IsNullOrEmpty(s.Status)).WithMessage("Please specify a valid status");
    }
}
