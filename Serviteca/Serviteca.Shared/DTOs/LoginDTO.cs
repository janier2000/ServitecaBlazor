using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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