using Domain;

namespace VWallet_API.Dtos;

public class UserGetDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public String Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Address UserAddress { get; set; }
}
