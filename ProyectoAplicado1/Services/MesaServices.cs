using Microsoft.EntityFrameworkCore;
using ProyectoAplicado1.Data;
using ProyectoAplicado1.Models;
using System.Linq.Expressions;

namespace ProyectoAplicado1.Services;

public class MesaServices
{
    private readonly ApplicationDbContext _contexto;
    public MesaServices(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }
    //Metodo Existe 
    public async Task<bool> Existe(int mesaId)
    {
        return await _contexto.Mesas.AnyAsync(m => m.MesaId == mesaId);
    }

    //Metodo el cual Nos Identifica si la mesa esta registrada en la base de datos
    public async Task<bool> ExisteNombreMesa(string Nombre, int id)
    {
        return await _contexto.Mesas
            .AnyAsync(m => m.Nombre.ToLower().Equals(Nombre.ToLower()) && m.MesaId != id);
    }

    //Metodo Insertar
    private async Task<bool> Insertar(Mesas mesas)
    {
        _contexto.Mesas.Add(mesas);
        return await _contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar 
    public async Task<bool> Modificar(Mesas mesas)
    {
        _contexto.Mesas.Update(mesas);
        var modificado = await _contexto.SaveChangesAsync() > 0;
        return modificado;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(Mesas mesas)
    {
        if (!await Existe(mesas.MesaId))
            return await Insertar(mesas);
        else
            return await Modificar(mesas);
    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int mesaId)
    {
        var eliminado = await _contexto.Mesas
            .Where(m => m.MesaId == mesaId)
            .ExecuteDeleteAsync();
        return eliminado > 0;
    }
    //Metodo Buscar
    public async Task<Mesas?> Buscar(int mesaId)
    {
        return await _contexto.Mesas
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MesaId == mesaId);
    }
    //Metodo Listar
    public async Task<List<Mesas>> Listar(Expression<Func<Mesas, bool>> criterio)
    {
        return await _contexto.Mesas
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ValidarMesaUnica(int mesaId, string nombre)
    {
        var MesaExistente = await _contexto.Mesas
                .Where(m => m.MesaId != mesaId)
                .Where(m => m.Nombre == nombre)
                .FirstOrDefaultAsync();

        return MesaExistente == null;
    }
}
