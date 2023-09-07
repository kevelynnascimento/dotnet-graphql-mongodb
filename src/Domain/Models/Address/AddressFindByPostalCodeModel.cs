namespace Domain.Models;

public class AddressFindByPostalCodeModel
{
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string Complement { get; set; }
    public string Reference { get; set; }
    public double[] GeoCoordinates { get; set; }
}