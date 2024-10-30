using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
	public class MetodoPagos
	{
		[Key]
		public int MetodoPagoId { get; set; }

		[Required(ErrorMessage = "Debe agregar el monto")]
		[Range(0.01, float.MaxValue, ErrorMessage = "Debe ser mayor a 0.")]
		public float Monto { get; set; }
       

        [ForeignKey("TarjetaId")]
		public Tarjetas tarjetas { get; set; }
	}
}
