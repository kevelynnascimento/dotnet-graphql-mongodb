using System.Diagnostics.CodeAnalysis;

namespace Domain.Shared.Exceptions;

[ExcludeFromCodeCoverage]
public class DomainException : Exception
{
    public DomainException() { }

    public DomainException(string message) : base(message) { }

    public DomainException(string message, Exception inner) : base(message, inner) { }
}
