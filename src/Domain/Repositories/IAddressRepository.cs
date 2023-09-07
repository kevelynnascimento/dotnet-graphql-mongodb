using Domain.Models;

namespace Domain.Repositories;

public interface IAddressRepository
{
    Task<AddressFindByPostalCodeModel> FindByPostalCodeAsync(string postalCode);
}
