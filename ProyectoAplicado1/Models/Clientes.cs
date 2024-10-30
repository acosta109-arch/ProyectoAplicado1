using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
	public class Clientes
	{
		[Key]
		public int ClienteId { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio")]
		[RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
		public string? Nombre { get; set; }

		[Required(ErrorMessage = "El Apellido es obligatorio")]
		[RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
		public string? Apellido { get; set; }

		[Required(ErrorMessage = "El Telefono es obligatorio")]
		[RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe contener 10 dígitos")]
		public string? Telefono { get; set; }

		[Required(ErrorMessage = "El correo es obligatorio")]
		[EmailAddress(ErrorMessage = "Por favor ingrese una dirección de correo válida")]
		public string? Correo { get; set; }

        [ForeignKey("TarjetaId")]
		public Tarjetas Tarjetas { get; set; }

		[ForeignKey("DireccionId")]
		public Direcciones Direcciones { get; set; }
	}
}
