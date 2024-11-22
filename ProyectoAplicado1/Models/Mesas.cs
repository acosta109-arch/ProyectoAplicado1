using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado1.Models;

public class Mesas
{
    [Key]
    public int MesaId { get; set; }

    [Required(ErrorMessage = "Favor llenar el campo Nombre")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras.")]
    public string Nombre { get; set; }

}
