using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models;

public class Items
{
    [Key]
    public int ItemId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria")]
    [RegularExpression(@"^[a-zA-Z\s,.]*$", ErrorMessage = "La descripción solo puede contener letras, espacios, punto y coma")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El tipo de item es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "El tipo solo puede contener letras y espacios")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, 200000, ErrorMessage = "El precio debe estar entre 0.01 y 200000")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "La foto es obligatoria.")]
    public string FotoURL { get; set; }

    // Relación con Ordenes a través de la tabla intermedia OrdenItem
    public List<OrderItem> OrdenItems { get; set; } = new List<OrderItem>();
}
