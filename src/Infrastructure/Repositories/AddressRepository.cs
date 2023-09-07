using Domain.Models;
using Domain.Repositories;
using Newtonsoft.Json;

namespace Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly HttpClient _httpClient;

    public AddressRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("external-api");
    }

    public async Task<AddressFindByPostalCodeModel> FindByPostalCodeAsync(string postalCode)
    {
        var url = new Uri($"{_httpClient.BaseAddress}/api/checkout/pub/postal-code/BRA/{postalCode}");

        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var model = JsonConvert.DeserializeObject<AddressFindByPostalCodeModel>(jsonResponse);

        return model;
    }

}
