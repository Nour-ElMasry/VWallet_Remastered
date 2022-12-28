using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain;

public class User : IdentityUser
{
    public String Name { get; set; }
    public DateTime DateOfBirth { get; set; }

    [ForeignKey("UserAddressId")]
    public Address UserAddress { get; set; }

    public IList<CreditCard> CreditCards { get; set; }
    public IList<CryptoCurrency> CryptoCurrencies { get; set; }


    public User(string name, DateTime dateOfBirth, Address userAddress)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        UserAddress = userAddress;
        CreditCards = new List<CreditCard>();
        CryptoCurrencies = new List<CryptoCurrency>();
    }

    public User()
    {
    }
}
