using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto;

public class LoginUserDto
{
    [Required]
    public String LoginName { get; set; }
    [Required]
    public String Password { get; set; }
}