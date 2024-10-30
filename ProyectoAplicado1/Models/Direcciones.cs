using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
	public class Direcciones
	{
		[Key]
		public int DireccionId { get; set; }

		public string? Descripcion { get; set; }
        [Required(ErrorMessage = "Se recomienda agregar una descripcion")]

        public string? Notas { get; set; }
	}
}
