using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models;

public class OrderItem
{
    [Key]
    public int OrdenItemId { get; set; }

    // Foreign Key para la relación con Ordenes
    public int OrdenId { get; set; }
    public Ordenes Orden { get; set; }

    // Foreign Key para la relación con Items
    public int ItemId { get; set; }
    public Items Item { get; set; }

    // Propiedad para la cantidad de este item en la orden
    [Required(ErrorMessage = "La cantidad es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1")]
    public int Cantidad { get; set; }

    // Propiedad calculada para el precio total de este item en la orden
    public decimal PrecioTotal => Cantidad * Item.Precio;
}
