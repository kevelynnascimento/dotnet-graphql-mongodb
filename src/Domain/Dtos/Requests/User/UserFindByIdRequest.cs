using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos.Requests.User;

[ExcludeFromCodeCoverage]
public class UserFindByIdRequest
{
    public string Id { get; set; }
}
