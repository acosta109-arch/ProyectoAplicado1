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
        // Incluir la mesa asociada con la orden
        return await _contexto.Ordenes
            .Include(o => o.DetalleItems)  // Incluyendo los detalles de la orden
            .Include(o => o.Mesa)          // Incluir la mesa asociada
            .FirstOrDefaultAsync(o => o.OrdenId == id);
    }

    public async Task<List<Ordenes>> Listar(Expression<Func<Ordenes, bool>> criterio)
    {
        // Incluir la mesa asociada con la orden
        return await _contexto.Ordenes.AsNoTracking()
            .Include(o => o.Mesa)          // Incluir la mesa asociada
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<Ordenes>> ListarOrdenes()
    {
        // Incluir la mesa asociada con la orden
        return await _contexto.Ordenes.AsNoTracking()
            .Include(o => o.Mesa)          // Incluir la mesa asociada
            .ToListAsync();
    }

    public async Task<int> ObtenerCantidadOrdenesAsync()
    {
        return await _contexto.Ordenes.CountAsync();
    }

    public async Task<bool> OrdenExiste(string nombreCliente)
    {
        return await _contexto.Ordenes
            .AnyAsync(o => o.NombreCliente == nombreCliente);
    }

    public async Task<Ordenes> GetOrdenById(int id)
    {
        // Incluir los ítems relacionados y la mesa asociada
        return await _contexto.Ordenes
            .Include(o => o.DetalleItems)  
            .Include(o => o.Mesa)         
            .FirstOrDefaultAsync(o => o.OrdenId == id);
    }
}
