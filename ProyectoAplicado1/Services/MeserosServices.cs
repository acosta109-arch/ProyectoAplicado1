using Microsoft.EntityFrameworkCore;
using ProyectoAplicado.Models;
using ProyectoAplicado1.Data;
using System.Linq.Expressions;

namespace ProyectoAplicado.Services;

public class MeserosServices
{
    private readonly ApplicationDbContext _contexto;

    public MeserosServices(ApplicationDbContext contexto)
    {
        _contexto = contexto; 
    }

    public async Task <bool>Existe(int MeseroId)
    {
        return await _contexto.Meseros.AnyAsync(M=>M.MeseroId == MeseroId);
    }

    private async Task <bool> Insertar(Meseros mesero)
    {
        _contexto.Meseros.Add(mesero);
        return await _contexto.SaveChangesAsync()>0;
    }
    private async Task<bool> Modificar(Meseros mesero)
    {
        _contexto.Meseros.Update(mesero);
        return await _contexto.SaveChangesAsync() > 0;
    }
    public async Task <bool> Guardar(Meseros mesero)
    {
        if(!await Existe(mesero.MeseroId))
            return await Insertar(mesero);
        else
            return await Modificar(mesero);
    }
    public async Task<bool>Eliminar(int id)
    {
        var eliminado = await _contexto.Meseros
            .Where(M => M.MeseroId == id)
            .ExecuteDeleteAsync();
        return eliminado > 0;
    }
    public async Task<Meseros?> Buscar(int id)
    {
        return await _contexto.Meseros
            .AsNoTracking()
            .FirstOrDefaultAsync(M=>M.MeseroId==id);
    }
    public async Task <List<Meseros>>Listar(Expression<Func<Meseros, bool>> Criterio)
    {
        return await _contexto.Meseros
           .AsNoTracking()
           .Where(Criterio)
           .ToListAsync();
    }
}

