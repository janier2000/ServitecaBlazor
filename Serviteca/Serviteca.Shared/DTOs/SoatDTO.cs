using Serviteca.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Serviteca.Shared.DTOs;

public class SoatDTO
{
    public int Id { get; set; }

    [Display(Name = "Aseguradora")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
    public int InsurerId { get; set; }

    [Display(Name = "Vehículo")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
    public int VehicleId { get; set; }

    [Display(Name = "Fecha registro")]
    [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime Date { get; set; }

    [Display(Name = "Fecha vencimiento")]
    [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime ExpirationDate { get; set; }

    [Display(Name = "Categoria tarifa")]
    public string RateCategory { get; set; } = null!;

    [Display(Name = "Datos de póliza")]
    public string PolicyData { get; set; } = null!;

    [Display(Name = "Precio")]
    public string Price { get; set; } = null!;
}