using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos.Requests.Address;

[ExcludeFromCodeCoverage]
public class AddressFindByPostalCodeRequest
{
    public string PostalCode { get; set; }
}
