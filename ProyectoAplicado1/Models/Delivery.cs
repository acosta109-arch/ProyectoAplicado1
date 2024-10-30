using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
	public class Delivery
	{
		[Key]
		public int DeliveryId { get; set; }

		public string? Nombre { get; set; }

		public string Apellido { get; set; }

		public string Telefono { get; set; }

		public string Correo { get; set; }

		[ForeignKey("PedidoId")]
		public Pedidos Pedidos { get; set; }
	}
}
