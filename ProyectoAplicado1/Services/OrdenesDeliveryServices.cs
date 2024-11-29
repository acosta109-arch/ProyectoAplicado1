using Microsoft.EntityFrameworkCore;
using ProyectoAplicado1.Data;
using ProyectoAplicado1.Models;
using System.Linq.Expressions;

namespace ProyectoAplicado1.Services;

public class OrdenesDeliveryServices
{
    private readonly ApplicationDbContext _contexto;

    public OrdenesDeliveryServices(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.OrdenesDelivery.AnyAsync(o => o.OrdenId == id);
    }

    private async Task<bool> Insertar(OrdenesDelivery ordenDelivery)
    {
        _contexto.OrdenesDelivery.Add(ordenDelivery);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(OrdenesDelivery ordenDelivery)
    {
        _contexto.OrdenesDelivery.Update(ordenDelivery);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(OrdenesDelivery ordenDelivery)
    {
        if (ordenDelivery.OrdenId == 0)
            return await Insertar(ordenDelivery);
        else
            return await Modificar(ordenDelivery);
    }

    public async Task<bool> Eliminar(int id)
    {
        var ordenDelivery = await _contexto.OrdenesDelivery.FindAsync(id);
        if (ordenDelivery != null)
        {
            _contexto.OrdenesDelivery.Remove(ordenDelivery);
            return await _contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<OrdenesDelivery?> Buscar(int id)
    {
        return await _contexto.OrdenesDelivery
            .Include(o => o.DetalleItems)  // Incluyendo los detalles de la orden
            .Include(o => o.Delivery)      // Incluir la relación con Delivery
            .FirstOrDefaultAsync(o => o.OrdenId == id);
    }

    public async Task<List<OrdenesDelivery>> Listar(Expression<Func<OrdenesDelivery, bool>> criterio)
    {
        return await _contexto.OrdenesDelivery.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<OrdenesDelivery>> ListarOrdenesDelivery()
    {
        return await _contexto.OrdenesDelivery.AsNoTracking()
            .Include(o => o.Delivery)  // Incluyendo la relación con Delivery
            .ToListAsync();
    }

    public async Task<int> ObtenerCantidadOrdenesDeliveryAsync()
    {
        return await _contexto.OrdenesDelivery.CountAsync();
    }

    public async Task<bool> OrdenDeliveryExiste(string direccionCliente)
    {
        return await _contexto.OrdenesDelivery
            .AnyAsync(o => o.Direccion == direccionCliente);
    }

    // Método para obtener todos los deliveries
    public async Task<List<Delivery>> ObtenerDeliveries()
    {
        return await _contexto.Delivery.AsNoTracking().ToListAsync();
    }
}
