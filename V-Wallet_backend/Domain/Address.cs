using System.ComponentModel.DataAnnotations;

namespace Domain;
public class Address
{
    [Key]
    public long AddressId { get; set; }

    public String Country { get; set; }
    public String City { get; set; }
    public String Street { get; set; }

    public Address(string country, string city, string street)
    {
        Country = country;
        City = city;
        Street = street;
    }
    public Address() { }
}

