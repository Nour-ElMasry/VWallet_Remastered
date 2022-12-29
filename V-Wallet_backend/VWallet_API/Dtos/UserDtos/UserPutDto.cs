using System.ComponentModel.DataAnnotations;

namespace Application.Dto;

public class UserPutDto
{
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
