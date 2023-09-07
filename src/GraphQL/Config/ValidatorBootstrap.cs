using Domain.Validators.User;
using FluentValidation;

namespace GraphQL.Config;

public static class ValidatorBootstrap
{
    public static void BootstrapValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<UserCreationRequestValidator>();
    }
}
