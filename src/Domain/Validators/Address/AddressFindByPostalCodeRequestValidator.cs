using System.Diagnostics.CodeAnalysis;
using Domain.Dtos.Requests.Address;
using FluentValidation;

namespace Domain.Validators.Address;

[ExcludeFromCodeCoverage]
public class AddressFindByPostalCodeRequestValidator : AbstractValidator<AddressFindByPostalCodeRequest>
{
    public AddressFindByPostalCodeRequestValidator()
    {
        RuleFor(x => x.PostalCode).NotNull().WithMessage("Postal code is required.");
    }
}
