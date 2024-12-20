﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProyectoAplicado.Models;
using ProyectoAplicado1.Data;
using System.Linq.Expressions;

namespace ProyectoAplicado.Services;

public class PostresServices
{
    private readonly ApplicationDbContext _contexto;
    public PostresServices(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }
    //Metodo Existe
    public async Task<bool> Existe(int postreId)
    {
        return await _contexto.Postres.AnyAsync();
    }
    //Metodo Insertar
    private async Task<bool> Insertar(Postres postre)
    {
        _contexto.Postres.Add(postre);
        return await _contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar
    public async Task<bool> Modificar(Postres postre)
    {
        _contexto.Update(postre);
        return await _contexto.SaveChangesAsync() > 0;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(Postres postre)
    {
        if (!await Existe(postre.PostreId))
            return await Insertar(postre);
        else
            return await Modificar(postre);
    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int id)
    {
        var eliminarpostre = await _contexto.Postres
            .Where(p => p.PostreId == id)
            .ExecuteDeleteAsync();
        return eliminarpostre > 0;
    }
    //Metodo Buscar
    public async Task<Postres?> Buscar(int postreId)
    {
        return await _contexto.Postres
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PostreId == postreId);
    }
    //Metodo Listar
    public async Task<List<Postres>> Listar(Expression<Func<Postres, bool>> criterio)
    {
        return await _contexto.Postres
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ValidarPostreUnico(int postreId, string nombre, string descripcion)
    {
        var bebidaExistente = await _contexto.Postres
            .Where(b => b.PostreId != postreId)
            .Where(b => b.Nombre == nombre || b.Descripcion == descripcion)
            .FirstOrDefaultAsync();

        return bebidaExistente == null;
    }
}