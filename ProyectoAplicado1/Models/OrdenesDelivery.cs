using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models;

public class OrdenesDelivery
{
    [Key]
    public int OrdenId { get; set; }

    [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
    public string NombreCliente { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    public string Direccion { get; set; }

    [Required(ErrorMessage = "El detalle de los items es obligatorio.")]
    public ICollection<DetalleItems> DetalleItems { get; set; } = new List<DetalleItems>();

    [Required(ErrorMessage = "El metodo de pago es obligatorio.")]
    public string MetodoPago { get; set; }

    public int Cantidad { get; set; }

    public decimal Total { get; set; }

    [Required]
    public int DeliveryId { get; set; }
    public Delivery Delivery { get; set; }
}
