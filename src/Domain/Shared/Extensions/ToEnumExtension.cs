using System.Diagnostics.CodeAnalysis;

namespace Domain.Shared.Extensions;

[ExcludeFromCodeCoverage]
public static class ToEnumExtension
{
    public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct, Enum
    {
        if (!Enum.TryParse<TEnum>(value, out var enumValue))
            throw new ArgumentException($"Invalid value '{value}' for enum type {typeof(TEnum)}.");

        var valuesOfEnum = (TEnum[])Enum.GetValues(typeof(TEnum));

        var isValid = valuesOfEnum.Contains(enumValue);

        if (!isValid)
            throw new ArgumentException($"Invalid value '{value}' for enum type {typeof(TEnum)}.");

        return enumValue;
    }
}
