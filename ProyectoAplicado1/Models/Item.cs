using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
        public string Nombre { get; set; }
        // Aqui poner el mensaje de error.
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "descripcion es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s,.]*$", ErrorMessage = "La descripción solo puede contener letras y espacios punto y coma")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 200000, ErrorMessage = "El precio debe estar entre 0.01 y 200000")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "La disponibilidad es obligatoria")]
        public bool Disponibilidad { get; set; }
        [Required(ErrorMessage = "La foto es obligatoria.")]
        public string FotoURL { get; set; }
    }
}
