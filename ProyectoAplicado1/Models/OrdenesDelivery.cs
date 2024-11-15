using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models;

public class OrdenesDelivery
{
    [Key]
    public int OrdenId { get; set; }

    public string NombreCliente { get; set; }

    public string Direccion { get; set; }

    public string Delivery { get; set; }

    public ICollection<DetalleItems> DetalleItems { get; set; } = new List<DetalleItems>();

    [Required(ErrorMessage = "El metodo de pago es obligatorio.")]
    public string MetodoPago { get; set; }

    public int Cantidad { get; set; }

    public decimal Total { get; set; }
}
