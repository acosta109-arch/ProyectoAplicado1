using System.ComponentModel.DataAnnotations;
namespace ProyectoAplicado1.Models
{
    public class Tarjetas
    {
        [Key]
        public int TarjetaId { get; set; }

        [Required(ErrorMessage = "El número de tarjeta es obligatorio")]
        [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "El número de tarjeta debe tener 16 dígitos")]
        public int NumeroTarjeta { get; set; }

        [Required(ErrorMessage = "El CVV es obligatorio")]
        [RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "El CVV debe tener 3 o 4 dígitos")]
        public int CVV { get; set; }

        //Tipo

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
        public DateOnly FechaVencimiento { get; set; }

        [Required(ErrorMessage = "El nombre del propietario es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$",
            ErrorMessage = "El nombre solo puede contener letras y espacios, entre 2 y 50 caracteres")]
        public string Propietario { get; set; }
    }
}