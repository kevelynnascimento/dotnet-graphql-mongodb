using System.Diagnostics.CodeAnalysis;
using Domain.Dtos.Requests.User;
using FluentValidation;

namespace Domain.Validators.User;

[ExcludeFromCodeCoverage]
public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");
        RuleFor(x => x.Name).NotNull().WithMessage("Name is required.");
    }
}
