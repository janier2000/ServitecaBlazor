using System.ComponentModel.DataAnnotations;

namespace Serviteca.Shared.Entities
{
    public class Brand
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public ICollection<Vehicle>? Vehicle { get; set; }
    }
}