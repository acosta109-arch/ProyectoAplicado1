using Microsoft.EntityFrameworkCore;
using ProyectoAplicado1.Data;
using ProyectoAplicado1.Models;
using System.Linq.Expressions;

namespace ProyectoAplicado1.Services;

public class DeliveryService
{
    private readonly ApplicationDbContext _contexto;

    public DeliveryService(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int deliveryId)
    {
        return await _contexto.Delivery.AnyAsync(d => d.DeliveryId == deliveryId);
    }

    public async Task<bool> Insertar(Delivery delivery)
    {
        _contexto.Add(delivery);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Delivery delivery)
    {
        _contexto.Update(delivery);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var eliminar = await _contexto.Delivery
            .Where(d => d.DeliveryId == id)
            .ExecuteDeleteAsync();
        return eliminar > 0;
    }

    public async Task<Delivery?> Buscar(int deliveryId)
    {
        return await _contexto.Delivery
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DeliveryId == deliveryId);
    }

    public async Task<List<Delivery>> Listar(Expression<Func<Delivery, bool>> criterio)
    {
        return await _contexto.Delivery
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Guardar(Delivery delivery)
    {
        if (await Existe(delivery.DeliveryId))
        {
            return await Modificar(delivery);
        }
        else
        {
            return await Insertar(delivery);
        }
    }

    public async Task<Delivery> ValidarDeliveryUnico(int deliveryId, string nombreCompleto, string telefono)
    {
        var existeItem = await _contexto.Delivery
                                        .FirstOrDefaultAsync(d => d.DeliveryId != deliveryId
                                                    && d.NombreCompleto == nombreCompleto
                                                    && d.Telefono == telefono);

        return existeItem; 
    }


}
