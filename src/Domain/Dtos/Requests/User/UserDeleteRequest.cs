using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos.Requests.User;

[ExcludeFromCodeCoverage]
public class UserDeleteRequest
{
    public string Id { get; set; }
}
