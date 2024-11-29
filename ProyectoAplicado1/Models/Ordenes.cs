using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models;

public class Ordenes
{
    [Key]
    public int OrdenId { get; set; }

    [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
    public string NombreCliente { get; set; }

    [Required(ErrorMessage = "La mesa es obligatoria.")]

    public ICollection<DetalleItems> DetalleItems { get; set; } = new List<DetalleItems>();

    [Required(ErrorMessage = "El metodo de pago es obligatorio.")]
    public string MetodoPago { get; set; }

    public int Cantidad { get; set; }

    public decimal Total { get; set; }

    [Required]
    public int MesaId { get; set; }

    public Mesas Mesa { get; set; }
}
