using ProyectoAplicado.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAplicado1.Models;

public class CarritoItem
{
    [Key]
    public int ArticuloId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    public decimal Subtotal => Cantidad * Precio;

    [ForeignKey("Comidas")]
    public int? ComidaId { get; set; }

    [ForeignKey("Bebidas")]
    public int? BebidaId { get; set; }

    [ForeignKey("PostreId")]
    public int? PostreId { get; set; }

    public Comidas Comida { get; set; }
    public Bebidas Bebida { get; set; }
    public Postres Postre { get; set; }

    public ArticuloTipo TipoArticulo {  get; set; }

    public enum ArticuloTipo
    {
        Comida,
        Bebida,
        Postre
    }
}
