using System.Diagnostics.CodeAnalysis;
using Domain.Dtos.Requests.User;
using FluentValidation;

namespace Domain.Validators.User;

[ExcludeFromCodeCoverage]
public class UserCreationRequestValidator : AbstractValidator<UserCreationRequest>
{
    public UserCreationRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Name is required.");
    }
}
