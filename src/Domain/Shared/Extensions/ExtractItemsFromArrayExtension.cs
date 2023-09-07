using System.Diagnostics.CodeAnalysis;

namespace Domain.Shared.Extensions;

[ExcludeFromCodeCoverage]
public static class ExtractItemsFromArrayExtension
{
    public static List<T> ExtractItems<T>(this IEnumerable<T[]> items)
    {
        if (items == null)
            return null;

        var result = new List<T>();

        foreach (var itemsFromArray in items)
        {
            if (itemsFromArray != null)
                result.AddRange(itemsFromArray);
        }

        return result;
    }
}
