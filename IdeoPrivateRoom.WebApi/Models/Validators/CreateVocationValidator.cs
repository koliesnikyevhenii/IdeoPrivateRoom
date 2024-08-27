using FluentValidation;
using IdeoPrivateRoom.WebApi.Models.Requests;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IdeoPrivateRoom.WebApi.Models.Validators;

public class CreateVocationValidator : AbstractValidator<CreateVocationRequest>
{
    public CreateVocationValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
    }
}
