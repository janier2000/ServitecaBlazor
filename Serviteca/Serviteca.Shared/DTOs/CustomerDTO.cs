using Serviteca.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Serviteca.Shared.DTOs;

public class CustomerDTO
{
    public int Id { get; set; }

    [Display(Name = "Tipo documento")]
    public int DocumentTypeId { get; set; }

    [Display(Name = "Documento")]
    [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
    [Required(ErrorMessage = "{0} es obligatorio.")]
    public string Document { get; set; } = null!;

    [Display(Name = "Nombres")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Apellidos")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
    [Required(ErrorMessage = "{0} es obligatorio.")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
    [Required(ErrorMessage = "{0} es obligatorio.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Genero ")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
    public int gender { get; set; } = 0!;

    [Display(Name = "Celular")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
    [Required(ErrorMessage = "{0} es obligatorio.")]
    public string phone { get; set; } = null!;

    [Display(Name = "Cliente desde")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
    [Required(ErrorMessage = "{0} es obligatorio.")]
    public DateTime ClientSince { get; set; }
}