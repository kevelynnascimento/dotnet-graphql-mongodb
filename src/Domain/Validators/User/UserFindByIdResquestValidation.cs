using System.Diagnostics.CodeAnalysis;
using Domain.Dtos.Requests.User;
using FluentValidation;

namespace Domain.Validators.User;

[ExcludeFromCodeCoverage]
public class UserFindByIdResquestValidation : AbstractValidator<UserFindByIdRequest>
{
    public UserFindByIdResquestValidation()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");
    }
}
