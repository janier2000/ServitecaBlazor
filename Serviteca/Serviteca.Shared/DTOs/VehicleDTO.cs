using System.ComponentModel.DataAnnotations;

namespace Serviteca.Shared.DTOs;

public class VehicleDTO
{
    public int Id { get; set; }

    [Display(Name = "Placa")]
    [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string Plate { get; set; } = null!;

    [Display(Name = "Modelo")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
    public int Model { get; set; }

    [Display(Name = "Fecha regreso vehiculo")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime ReturnDate { get; set; }

    [Display(Name = "Kilometraje actual")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
    public int CurrentKm { get; set; }

    [Display(Name = "Cliente")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
    public int CustomerId { get; set; }

    [Display(Name = "Tipo")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
    public int TypeVId { get; set; }

    [Display(Name = "Uso")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
    public int UseId { get; set; }

    [Display(Name = "Marca")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public string CustomerName { get; set; } = null!;
}