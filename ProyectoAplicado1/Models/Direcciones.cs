using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
	public class Direcciones
	{
		[Key]
		public int DireccionId { get; set; }

		[Required(ErrorMessage = "Se recomienda agregar una descripcion")]
		public string? Descripcion { get; set; }

        public string? Notas { get; set; }
	}
}
