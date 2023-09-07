using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos.Responses.User;

[ExcludeFromCodeCoverage]
public class UserFindByIdResponse
{
    public string Name { get; set; }
}
