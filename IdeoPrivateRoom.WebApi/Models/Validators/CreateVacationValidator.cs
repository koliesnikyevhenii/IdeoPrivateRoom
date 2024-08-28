using FluentValidation;
using IdeoPrivateRoom.WebApi.Models.Requests;

namespace IdeoPrivateRoom.WebApi.Models.Validators;

public class CreateVacationValidator : AbstractValidator<CreateVacationRequest>
{
    public CreateVacationValidator()
    {
        RuleFor(x => x.UserId).Must(x => Guid.TryParse(x, out var _)).WithMessage("Please specify a valid User Id.");
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty().GreaterThan(x => x.StartDate).WithMessage("End date cannot be earlier than the start date.");
    }
}
