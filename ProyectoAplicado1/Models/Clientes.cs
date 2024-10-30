using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
	public class Clientes
	{
		[Key]
		public int ClienteId { get; set; }

		public string Clave { get; set; }

		public string? Nombre { get; set; }

		public string Apellido { get; set; }

		public string Telefono { get; set; }

		public string Correo { get; set; }

		[ForeignKey("TarjetaId")]
		public Tarjetas Tarjetas { get; set; }

		[ForeignKey("DireccionId")]
		public Direcciones Direcciones { get; set; }
	}
}
