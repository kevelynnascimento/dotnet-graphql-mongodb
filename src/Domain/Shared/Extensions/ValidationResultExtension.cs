using System.Diagnostics.CodeAnalysis;
using Domain.Shared.Exceptions;
using FluentValidation.Results;

namespace Domain.Shared.Extensions;

[ExcludeFromCodeCoverage]
public static class ValidationResultExtension
{
    public static string GetErrorMessage(this ValidationResult validationResult)
    {
        var error = validationResult.Errors.Select(x => x.ErrorMessage);
        var errorMessage = string.Join(", ", error);
        return errorMessage;
    }
}
