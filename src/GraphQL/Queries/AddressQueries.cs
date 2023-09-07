using Domain.Dtos.Requests.Address;
using Domain.Dtos.Responses.Address;
using Domain.Services.Interfaces;

namespace GraphQL.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public class AddressQueries
{
    private readonly IAddressService _addressService;

    public AddressQueries(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [GraphQLName("getAddressByPostalCode")]
    public async Task<AddressFindByPostalCodeResponse> FindByPostalCodeAsync(AddressFindByPostalCodeRequest request)
    {
        var response = await _addressService.FindByPostalCodeAsync(request);

        return response;
    }
}
