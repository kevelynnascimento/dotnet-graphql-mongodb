using AutoMapper;
using Domain.Dtos.Requests.Address;
using Domain.Dtos.Responses.Address;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Domain.Shared.Exceptions;
using FizzWare.NBuilder;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace UnitTest.Services;

public class AddressServiceTest
{
    private readonly AddressService _addressService;
    private readonly Mock<IAddressRepository> _addressRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidator<AddressFindByPostalCodeRequest>> _addressFindByPostalCodeRequestValidatorMock;

    public AddressServiceTest()
    {
        _addressRepositoryMock = new Mock<IAddressRepository>();
        _mapperMock = new Mock<IMapper>();
        _addressFindByPostalCodeRequestValidatorMock = new Mock<IValidator<AddressFindByPostalCodeRequest>>();

        _addressService = new AddressService(
            _addressRepositoryMock.Object,
            _mapperMock.Object,
            _addressFindByPostalCodeRequestValidatorMock.Object
        );
    }

    public class FindByPostalCodeAsync : AddressServiceTest
    {
        [Fact]
        public async Task FindByPostalCodeAsync_ReturningAddress()
        {
            var request = Builder<AddressFindByPostalCodeRequest>.CreateNew().Build();
            var validation = Builder<ValidationResult>.CreateNew().Build();

            var addressFindByPostalCodeModel = Builder<AddressFindByPostalCodeModel>
                        .CreateNew()
                        .With(a => a.Street = "123 Main St")
                        .With(a => a.City = "Example City")
                        .With(a => a.GeoCoordinates = Builder<double>
                            .CreateListOfSize(2)
                            .All()
                            .Build().ToArray())
                        .Build();

            var geoCoordinates = Builder<long>.CreateListOfSize(2).All().Build().ToArray();

            var addressFindByPostalCodeResponse = Builder<AddressFindByPostalCodeResponse>.CreateNew().Build();

            _addressFindByPostalCodeRequestValidatorMock.Setup(x => x.Validate(request)).Returns(validation);
            _addressRepositoryMock.Setup(x => x.FindByPostalCodeAsync(request.PostalCode)).ReturnsAsync(addressFindByPostalCodeModel);
            _mapperMock.Setup(x => x.Map<AddressFindByPostalCodeResponse>(addressFindByPostalCodeModel)).Returns(addressFindByPostalCodeResponse);

            var response = await _addressService.FindByPostalCodeAsync(request);

            response.Should().BeEquivalentTo(addressFindByPostalCodeResponse);

            _addressFindByPostalCodeRequestValidatorMock.Verify(x => x.Validate(It.IsAny<AddressFindByPostalCodeRequest>()), Times.Once);
            _addressRepositoryMock.Verify(x => x.FindByPostalCodeAsync(It.IsAny<string>()), Times.Once);
            _mapperMock.Verify(x => x.Map<AddressFindByPostalCodeResponse>(It.IsAny<AddressFindByPostalCodeModel>()), Times.Once);
        }

        [Fact]
        public async Task FindByPostalCodeAsync_WithValidationError()
        {
            var request = Builder<AddressFindByPostalCodeRequest>.CreateNew().Build();
            var erros = Builder<ValidationFailure>.CreateListOfSize(2).Build();
            var validation = Builder<ValidationResult>.CreateNew().With(x => x.Errors = erros.ToList()).Build();

            _addressFindByPostalCodeRequestValidatorMock.Setup(x => x.Validate(request)).Returns(validation);

            await Assert.ThrowsAsync<DomainException>(async () => await _addressService.FindByPostalCodeAsync(request));

            _addressFindByPostalCodeRequestValidatorMock.Verify(x => x.Validate(It.IsAny<AddressFindByPostalCodeRequest>()), Times.Once);
            _addressRepositoryMock.Verify(x => x.FindByPostalCodeAsync(It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(x => x.Map<AddressFindByPostalCodeResponse>(It.IsAny<AddressFindByPostalCodeModel>()), Times.Never);
        }

        [Fact]
        public async Task FindByPostalCodeAsync_WithNoAddressesError()
        {
            var request = Builder<AddressFindByPostalCodeRequest>.CreateNew().Build();
            var validation = Builder<ValidationResult>.CreateNew().Build();

            var addressFindByPostalCodeModel = Builder<AddressFindByPostalCodeModel>
                        .CreateNew()
                        .With(a => a.Street = "123 Main St")
                        .With(a => a.City = "Example City")
                        .With(a => a.GeoCoordinates = new double[] { })
                        .Build();

            var geoCoordinates = Builder<long>.CreateListOfSize(2).All().Build().ToArray();

            var addressFindByPostalCodeResponse = Builder<AddressFindByPostalCodeResponse>.CreateNew().Build();

            _addressFindByPostalCodeRequestValidatorMock.Setup(x => x.Validate(request)).Returns(validation);
            _addressRepositoryMock.Setup(x => x.FindByPostalCodeAsync(request.PostalCode)).ReturnsAsync(addressFindByPostalCodeModel);
            _mapperMock.Setup(x => x.Map<AddressFindByPostalCodeResponse>(addressFindByPostalCodeModel)).Returns(addressFindByPostalCodeResponse);

            await Assert.ThrowsAsync<ErrorCodeException>(async () => await _addressService.FindByPostalCodeAsync(request));

            _addressFindByPostalCodeRequestValidatorMock.Verify(x => x.Validate(It.IsAny<AddressFindByPostalCodeRequest>()), Times.Once);
            _addressRepositoryMock.Verify(x => x.FindByPostalCodeAsync(It.IsAny<string>()), Times.Once);
            _mapperMock.Verify(x => x.Map<AddressFindByPostalCodeResponse>(It.IsAny<AddressFindByPostalCodeModel>()), Times.Never);
        }
    }
}
