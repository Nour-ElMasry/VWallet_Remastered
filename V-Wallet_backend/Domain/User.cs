using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain;

public class User : IdentityUser
{
    public String Name { get; set; }
    public DateTime DateOfBirth { get; set; }

    [ForeignKey("UserAddressId")]
    public Address UserAddress { get; set; }

    public List<CreditCard> CreditCards { get; set; } = new();
    public List<CryptoCurrency> CryptoCurrencies { get; set; } = new();


    public User(string name, DateTime dateOfBirth, Address userAddress)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        UserAddress = userAddress;
    }

    public User()
    {
    }
}
