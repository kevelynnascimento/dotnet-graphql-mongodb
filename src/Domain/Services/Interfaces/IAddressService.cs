using Domain.Dtos.Requests.Address;
using Domain.Dtos.Responses.Address;

namespace Domain.Services.Interfaces;

public interface IAddressService
{
    Task<AddressFindByPostalCodeResponse> FindByPostalCodeAsync(AddressFindByPostalCodeRequest request);
}
