using AutoMapper;
using Domain.Dtos.Requests.Address;
using Domain.Dtos.Responses.Address;
using Domain.Repositories;
using Domain.Services.Interfaces;
using Domain.Shared.Exceptions;
using Domain.Shared.Extensions;
using FluentValidation;

namespace Domain.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AddressFindByPostalCodeRequest> _addressFindByPostalCodeRequestValidator;

    public AddressService
    (
        IAddressRepository addressRepository,
        IMapper mapper,
        IValidator<AddressFindByPostalCodeRequest> addressFindByPostalCodeRequestValidator
    )
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
        _addressFindByPostalCodeRequestValidator = addressFindByPostalCodeRequestValidator;
    }

    public async Task<AddressFindByPostalCodeResponse> FindByPostalCodeAsync(AddressFindByPostalCodeRequest request)
    {
        var validation = _addressFindByPostalCodeRequestValidator.Validate(request);

        if (!validation.IsValid)
            throw new DomainException(validation.GetErrorMessage());

        var address = await _addressRepository.FindByPostalCodeAsync(request.PostalCode);

        if (address != null && address.GeoCoordinates != null && !address.GeoCoordinates.Any())
            throw new ErrorCodeException(ErrorCodes.ZIP_CODE_NOT_FOUND);

        var response = _mapper.Map<AddressFindByPostalCodeResponse>(address);

        return response;
    }
}
