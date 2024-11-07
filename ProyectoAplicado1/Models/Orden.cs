using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
    public class Orden
    {
        [Key]
        public int OrdenId { get; set; }
        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0.01, 200000.00, ErrorMessage = "El precio debe estar entre 0.01 y 200000.00.")]
        public decimal Total { get; set; }
    }
}
