using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
	public class MetodoPagos
	{
		[Key]
		public int MetodoPagoId { get; set; }

		public float Monto { get; set; }
        [Required(ErrorMessage = "Debe agregar el monto")]
        [RegularExpression(@"^[1-9]\d*\.?\d*$", ErrorMessage = "El monto debe ser mayor que 0")]

        [ForeignKey("TarjetaId")]
		public Tarjetas tarjetas { get; set; }
	}
}
