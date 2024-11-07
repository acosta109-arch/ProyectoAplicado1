using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
    public class OrdenDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public int CobroId { get; set; }
        public int PrestamoId { get; set; }
        public double ValorCobrado { get; set; }
        [ForeignKey("CobroId")]
        [InverseProperty("CobrosDetalle")]
        public virtual Orden Orden { get; set; } = null!;
    }
}
