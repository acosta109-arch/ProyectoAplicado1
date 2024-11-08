using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
    }
}
