using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models;

public class Delivery
{
    [Key]
    public int DeliveryId { get; set; }

    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre completo solo puede contener letras y espacios.")]
    public string NombreCompleto { get; set; }

    [Required(ErrorMessage = "La URL de la foto es obligatoria.")]
    public string FotoURL { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "El turno es obligatorio.")]
    public string Turno { get; set; }

    [Required(ErrorMessage = "La ruta es obligatoria.")]
    public string Ruta { get; set; }
}

