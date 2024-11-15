using Microsoft.EntityFrameworkCore;
using ProyectoAplicado.Models;
using ProyectoAplicado1.Data;
using ProyectoAplicado1.Models;
using System.Linq;
using System.Linq.Expressions;

namespace ProyectoAplicado1.Services;

public class ItemsService
{
    private readonly ApplicationDbContext _contexto;

    public ItemsService(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    // Método Existe
    public async Task<bool> Existe(int itemId)
    {
        return await _contexto.Items.AnyAsync(i => i.ItemId == itemId);
    }

    // Método ExisteNombre
    public async Task<bool> ExisteNombre(string nombre, int itemId)
    {
        return await _contexto.Items
            .AnyAsync(i => i.Nombre.ToLower().Equals(nombre.ToLower()) && i.ItemId != itemId);
    }

    // Método Insertar
    private async Task<bool> Insertar(Items item)
    {
        _contexto.Items.Add(item);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Método Modificar
    public async Task<bool> Modificar(Items item)
    {
        _contexto.Items.Update(item);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Método Guardar
    public async Task<bool> Guardar(Items item)
    {
        if (!await Existe(item.ItemId))
            return await Insertar(item);
        else
            return await Modificar(item);
    }

    // Método Eliminar
    public async Task<bool> Eliminar(int itemId)
    {
        var eliminado = await _contexto.Items
            .Where(i => i.ItemId == itemId)
            .ExecuteDeleteAsync();
        return eliminado > 0;
    }

    // Método Buscar
    public async Task<Items?> Buscar(int itemId)
    {
        return await _contexto.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.ItemId == itemId);
    }

    // Método Listar
    public async Task<List<Items>> Listar(Expression<Func<Items, bool>> criterio)
    {
        return await _contexto.Items
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<Items> ValidarItemUnico(int itemId, string nombre, string descripcion)
    {
        // Buscar un ítem que tenga el mismo nombre o descripción
        var itemExistente = await _contexto.Items
            .Where(i => (i.Nombre == nombre || i.Descripcion == descripcion) && i.ItemId != itemId)
            .FirstOrDefaultAsync();

        return itemExistente; // Si es null, el ítem es único
    }
}
