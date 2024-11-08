using Microsoft.EntityFrameworkCore;
using ProyectoAplicado1.Data;
using ProyectoAplicado1.Models;
using System.Linq.Expressions;

namespace ProyectoAplicado1.Services;

public class OrdenesServices
{
    private readonly ApplicationDbContext _contexto;

    public OrdenesServices(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    // Método Existe
    public async Task<bool> Existe(int ordenId)
    {
        return await _contexto.Ordenes.AnyAsync(o => o.OrdenId == ordenId);
    }

    // Método Insertar
    private async Task<bool> Insertar(Ordenes orden)
    {
        _contexto.Ordenes.Add(orden);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Método Modificar
    public async Task<bool> Modificar(Ordenes orden)
    {
        _contexto.Ordenes.Update(orden);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Método Guardar
    public async Task<bool> Guardar(Ordenes orden)
    {
        if (!await Existe(orden.OrdenId))
            return await Insertar(orden);
        else
            return await Modificar(orden);
    }

    // Método Eliminar
    public async Task<bool> Eliminar(int ordenId)
    {
        var eliminado = await _contexto.Ordenes
            .Where(o => o.OrdenId == ordenId)
            .ExecuteDeleteAsync();
        return eliminado > 0;
    }

    // Método Buscar
    public async Task<Ordenes?> Buscar(int ordenId)
    {
        return await _contexto.Ordenes
            .AsNoTracking()
            .Include(o => o.OrdenItems) // Incluye los items de la orden
            .FirstOrDefaultAsync(o => o.OrdenId == ordenId);
    }

    // Método Listar
    public async Task<List<Ordenes>> Listar(Expression<Func<Ordenes, bool>> criterio)
    {
        return await _contexto.Ordenes
            .AsNoTracking()
            .Where(criterio)
            .Include(o => o.OrdenItems) // Incluye los items de la orden
            .ToListAsync();
    }
}
