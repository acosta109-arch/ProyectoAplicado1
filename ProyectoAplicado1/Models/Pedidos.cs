using ProyectoAplicado1.Components.Pages;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProyectoAplicado.Models;

namespace ProyectoAplicado1.Models
{
	public class Pedidos
	{
		[Key]
		public int PedidoId { get; set; }

		[ForeignKey("ComidaId")]
		public List<Comidas> Comidas { get; set; }

		[ForeignKey("BedidaId")]
		public List<Bebidas> Bedidas { get; set; }

		[ForeignKey("PostreId")]
		public List<Postres> Postres { get; set; }
		public MetodoPagos MetodoPagos { get; set; }

		//Estado

		public TimeOnly HoraPedido { get; set; }

		[ForeignKey("ClienteId")]
		public Clientes Clientes { get; set; }
	}
}
