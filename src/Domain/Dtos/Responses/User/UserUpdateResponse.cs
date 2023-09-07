using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos.Responses.User;

[ExcludeFromCodeCoverage]
public class UserUpdateResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
}
