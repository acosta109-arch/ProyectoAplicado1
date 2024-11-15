using Microsoft.EntityFrameworkCore;
using ProyectoAplicado1.Data;
using ProyectoAplicado1.Models;
using System.Linq.Expressions;

namespace ProyectoAplicado1.Services;

public class DetalleItemsServices
{
    private readonly ApplicationDbContext _contexto;

    public DetalleItemsServices(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    // Método Existe
    public async Task<bool> Existe(int detalleItemId)
    {
        return await _contexto.DetalleItems.AnyAsync(d => d.DetalleItemId == detalleItemId);
    }

    // Método ExisteNombre
    public async Task<bool> ExisteNombre(string nombre, int detalleItemId)
    {
        return await _contexto.DetalleItems
            .AnyAsync(d => d.Nombre.ToLower().Equals(nombre.ToLower()) && d.DetalleItemId != detalleItemId);
    }

    // Método Insertar
    private async Task<bool> Insertar(DetalleItems detalleItem)
    {
        _contexto.DetalleItems.Add(detalleItem);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Método Modificar
    public async Task<bool> Modificar(DetalleItems detalleItem)
    {
        _contexto.DetalleItems.Update(detalleItem);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Método Guardar
    public async Task<bool> Guardar(DetalleItems detalleItem)
    {
        if (!await Existe(detalleItem.DetalleItemId))
            return await Insertar(detalleItem);
        else
            return await Modificar(detalleItem);
    }

    // Método Eliminar
    public async Task<bool> Eliminar(int detalleItemId)
    {
        var eliminado = await _contexto.DetalleItems
            .Where(d => d.DetalleItemId == detalleItemId)
            .ExecuteDeleteAsync();
        return eliminado > 0;
    }

    // Método Buscar
    public async Task<DetalleItems?> Buscar(int detalleItemId)
    {
        return await _contexto.DetalleItems
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DetalleItemId == detalleItemId);
    }

    // Método Listar
    public async Task<List<DetalleItems>> Listar(Expression<Func<DetalleItems, bool>> criterio)
    {
        return await _contexto.DetalleItems
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    // Método para validar si el detalle de un ítem es único (por nombre o descripción)
    public async Task<DetalleItems?> ValidarDetalleItemUnico(int detalleItemId, string nombre, string descripcion)
    {
        // Buscar un detalle que tenga el mismo nombre o descripción
        var detalleItemExistente = await _contexto.DetalleItems
            .Where(d => (d.Nombre == nombre || d.Descripcion == descripcion) && d.DetalleItemId != detalleItemId)
            .FirstOrDefaultAsync();

        return detalleItemExistente; // Si es null, el detalle ítem es único
    }

    public async Task<List<DetalleItems>> ObtenerItemsPorTipoAsync(string tipo)
    {
        return await _contexto.DetalleItems
            .Where(i => i.Tipo == tipo)
            .ToListAsync();
    }

    public async Task<List<DetalleItems>> GetAllItems()
    {
        return await _contexto.DetalleItems.ToListAsync();
    }
}
