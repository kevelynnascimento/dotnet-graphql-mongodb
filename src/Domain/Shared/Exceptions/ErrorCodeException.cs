using System.Diagnostics.CodeAnalysis;

namespace Domain.Shared.Exceptions;

[ExcludeFromCodeCoverage]
public class ErrorCodeException : Exception
{
    public ErrorCodeException() { }

    public ErrorCodeException(string errorCode) : base($"[ERROR_CODE]: ({errorCode})") { }

    public ErrorCodeException(string errorCode, Exception inner) : base($"[ERROR_CODE]: ({errorCode})", inner) { }
}
