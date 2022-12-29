using System.ComponentModel.DataAnnotations;

namespace VWallet_API.Dtos.UserDtos;

public class UserPostDto
{
    [Required]
    [MaxLength(20)]
    [MinLength(4)]
    [RegularExpression("^[a-zA-Z0-9]+$")]
    public string Username { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(8)]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
    public string Password { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [MaxLength(30)]
    public string Country { get; set; }

    [Required]
    [MaxLength(30)]
    public String City { get; set; }

    [Required]
    [MaxLength(30)]
    public String Street { get; set; }
}
