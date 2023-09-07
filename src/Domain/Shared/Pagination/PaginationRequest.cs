using System.Diagnostics.CodeAnalysis;

namespace Domain.Shared.Pagination;

[ExcludeFromCodeCoverage]
public class PaginationRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}
