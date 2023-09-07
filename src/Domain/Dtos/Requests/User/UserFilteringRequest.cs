using System.Diagnostics.CodeAnalysis;
using Domain.Shared.Pagination;

namespace Domain.Dtos.Requests.User;

[ExcludeFromCodeCoverage]
public class UserFilteringRequest : PaginationRequest
{
    public string Name { get; set; }
}
