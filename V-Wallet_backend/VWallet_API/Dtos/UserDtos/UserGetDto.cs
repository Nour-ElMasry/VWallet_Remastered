using Domain;

namespace VWallet_API.Dtos.UserDtos;

public class UserGetDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Address UserAddress { get; set; }
}
