using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
	public class MetodoPagos
	{
		[Key]
		public int MetodoPagoId { get; set; }

		public float Monto { get; set; }

		[ForeignKey("TarjetaId")]
		public Tarjetas tarjetas { get; set; }
	}
}
