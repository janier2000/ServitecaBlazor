using System.ComponentModel.DataAnnotations;

namespace Serviteca.Shared.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }


        public Customer? Customer { get; set; }

        [Display(Name = "Cliente")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CustomerId { get; set; }


        [Display(Name = "Placa")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Plate { get; set; } = null!;


        [Display(Name = "Modelo")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int Model { get; set; }


        [Display(Name = "Fecha regreso vehiculo")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string ReturnDate { get; set; } = null!;


        [Display(Name = "Kilometraje actual")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CurrentKm { get; set; }

        public VehicleType? VehicleType { get; set; }

        [Display(Name = "Tipo de vehiculo")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int VehicleTypeId { get; set; }

        public CartType? CartType { get; set; }

        [Display(Name = "Tipo de carro")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CartTypeId { get; set; }

        public Brand? Brand { get; set; }

        [Display(Name = "Marca")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int BrandId { get; set; }

    }
}