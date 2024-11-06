using ProyectoAplicado1.Models;

namespace ProyectoAplicado1.Services;

public class CarritoService
{
    private List<CarritoItem> _items = new List<CarritoItem>();

    public IReadOnlyList<CarritoItem> ObtenerItems() => _items;

    public void AgregarItem(CarritoItem item)
    {
        var existente = _items.FirstOrDefault(i =>
        (i.ComidaId == item.ComidaId && item.TipoArticulo == CarritoItem.ArticuloTipo.Comida) ||
        (i.BebidaId == item.BebidaId && item.TipoArticulo == CarritoItem.ArticuloTipo.Bebida) ||
        (i.PostreId == item.PostreId && item.TipoArticulo == CarritoItem.ArticuloTipo.Postre)
        );

        if (existente != null)
        {
            existente.Cantidad += item.Cantidad;
        }
        else
        {
            _items.Add(item);
        }
    }

    public void EliminarItem(int articuloId, CarritoItem.ArticuloTipo tipoArticulo)
    {
        var item = _items.FirstOrDefault(i =>
        (i.ComidaId == articuloId && tipoArticulo == CarritoItem.ArticuloTipo.Comida) ||
        (i.BebidaId == articuloId && tipoArticulo == CarritoItem.ArticuloTipo.Bebida) ||
        (i.PostreId == articuloId && tipoArticulo == CarritoItem.ArticuloTipo.Postre)
        );

        if (item != null)
        {
            _items.Remove(item);
        }

    }

    public decimal CalcularTotal()
    {
        return _items.Sum(i => i.Subtotal);
    }

    public void LimpiarCarrito()
    {
        _items.Clear();
    }
}
