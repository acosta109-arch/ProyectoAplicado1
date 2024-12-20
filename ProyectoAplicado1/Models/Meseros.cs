﻿using System.ComponentModel.DataAnnotations;

namespace ProyectoAplicado.Models;

public class Meseros
{
    [Key]
    public int MeseroId { get; set; }

    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre completo solo puede contener letras.")]
    public string NombreCompleto { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener exactamente 10 dígitos.")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El estado solo puede contener letras.")]
    public string Estado { get; set; }

    [Required(ErrorMessage = "La foto es obligatoria.")]
    public string FotoURL { get; set; }
}
