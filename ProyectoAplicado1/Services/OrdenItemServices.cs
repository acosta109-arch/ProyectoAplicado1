using Microsoft.EntityFrameworkCore;
using ProyectoAplicado1.Data;
using ProyectoAplicado1.Models;
using System.Linq.Expressions;

namespace ProyectoAplicado1.Services;

public class OrdenItemServices
{
    private readonly ApplicationDbContext _contexto;

    public OrdenItemServices(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    // Método Existe
    public async Task<bool> Existe(int ordenItemId)
    {
        return await _contexto.OrdenItems.AnyAsync(oi => oi.OrdenItemId == ordenItemId);
    }

    // Método Insertar
    private async Task<bool> Insertar(OrderItem ordenItem)
    {
        _contexto.OrdenItems.Add(ordenItem);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Método Modificar
    public async Task<bool> Modificar(OrderItem ordenItem)
    {
        _contexto.OrdenItems.Update(ordenItem);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Método Guardar
    public async Task<bool> Guardar(OrderItem ordenItem)
    {
        if (!await Existe(ordenItem.OrdenItemId))
            return await Insertar(ordenItem);
        else
            return await Modificar(ordenItem);
    }

    // Método Eliminar
    public async Task<bool> Eliminar(int ordenItemId)
    {
        var eliminado = await _contexto.OrdenItems
            .Where(oi => oi.OrdenItemId == ordenItemId)
            .ExecuteDeleteAsync();
        return eliminado > 0;
    }

    // Método Buscar
    public async Task<OrderItem?> Buscar(int ordenItemId)
    {
        return await _contexto.OrdenItems
            .AsNoTracking()
            .Include(oi => oi.Item) // Incluye el detalle del item
            .FirstOrDefaultAsync(oi => oi.OrdenItemId == ordenItemId);
    }

    // Método Listar
    public async Task<List<OrderItem>> Listar(Expression<Func<OrderItem, bool>> criterio)
    {
        return await _contexto.OrdenItems
            .AsNoTracking()
            .Where(criterio)
            .Include(oi => oi.Item) // Incluye el detalle del item
            .ToListAsync();
    }
}
