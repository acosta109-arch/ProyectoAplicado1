using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models;

public class Reportes
{
    [Key]
    public int ReporteId { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras.")]
    public string Nombre { get; set; }
    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "La descripción solo puede contener letras.")]
    public string Descripcion { get; set; }
}