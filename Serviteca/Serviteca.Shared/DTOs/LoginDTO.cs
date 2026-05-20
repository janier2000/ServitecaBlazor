using System.ComponentModel.DataAnnotations;

namespace Serviteca.Shared.DTOs;

public class LoginDTO
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessageResourceName = "ValidEmail")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [Required(ErrorMessageResourceName = "RequiredField")]
    [MinLength(6, ErrorMessageResourceName = "MinLength")]
    public string Password { get; set; } = null!;
}