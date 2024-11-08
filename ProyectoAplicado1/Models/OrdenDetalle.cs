using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
    public class OrdenDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public int ItemId { get; set; }
        public int Cantidad { get; set; }
        public double PrecioActual { get; set; }
        [ForeignKey("CobroId")]
        [InverseProperty("CobrosDetalle")]
        public virtual Orden Orden { get; set; } = null!;
    }
}
