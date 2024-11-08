using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models;

public class Ordenes
{
    [Key]
    public int OrdenId { get; set; }

    public DateTime Fecha { get; set; }

    public string Cliente { get; set; }

    public decimal Total { get; set; }

    // Relación con Items a través de la tabla intermedia OrdenItem
    public List<OrderItem> OrdenItems { get; set; } = new List<OrderItem>();
}
