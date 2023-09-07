using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos.Requests.User;

[ExcludeFromCodeCoverage]
public class UserUpdateRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
}
