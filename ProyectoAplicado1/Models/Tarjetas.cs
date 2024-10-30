using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
	public class Tarjetas
	{
		[Key]
		public int TarjetaId { get; set; }

		public int NumeroTarjeta { get; set; }

		public int CVV { get; set; }

		//Tipo

		public DateOnly FechaVencimiento { get; set; }

		public string Propietario { get; set; }
	}
}
