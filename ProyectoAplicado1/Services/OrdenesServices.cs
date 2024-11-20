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

    public async Task<bool> Existe(int id)
    {
        return await _contexto.Ordenes.AnyAsync(o => o.OrdenId == id);
    }

    private async Task<bool> Insertar(Ordenes orden)
    {
        _contexto.Ordenes.Add(orden);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Ordenes orden)
    {
        _contexto.Ordenes.Update(orden);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Ordenes orden)
    {
        if (orden.OrdenId == 0)
            return await Insertar(orden);
        else
            return await Modificar(orden);
    }

    public async Task<bool> Eliminar(int id)
    {
        var orden = await _contexto.Ordenes.FindAsync(id);
        if (orden != null)
        {
            _contexto.Ordenes.Remove(orden);
            return await _contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<Ordenes?> Buscar(int id)
    {
        return await _contexto.Ordenes
            .Include(o => o.DetalleItems)  // Incluyendo los detalles de la orden
            .FirstOrDefaultAsync(o => o.OrdenId == id);
    }

    public async Task<List<Ordenes>> Listar(Expression<Func<Ordenes, bool>> criterio)
    {
        return await _contexto.Ordenes.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<Ordenes>> ListarOrdenes()
    {
        return await _contexto.Ordenes.AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> ObtenerCantidadOrdenesAsync()
    {
        return await _contexto.Ordenes.CountAsync();
    }

    public async Task<bool> OrdenExiste(string nombreCliente, string mesa)
    {
        return await _contexto.Ordenes
            .AnyAsync(o => o.NombreCliente == nombreCliente || o.Mesa == mesa);
    }

    public async Task<Ordenes> GetOrdenById(int id)
    {
        return await _contexto.Ordenes
            .Include(o => o.DetalleItems) // Incluye los ítems relacionados
            .FirstOrDefaultAsync(o => o.OrdenId == id);
    }
}
