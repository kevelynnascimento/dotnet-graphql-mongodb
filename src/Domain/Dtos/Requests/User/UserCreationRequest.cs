using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos.Requests.User;

[ExcludeFromCodeCoverage]
public class UserCreationRequest
{
    public string Name { get; set; }
}
