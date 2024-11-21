using Microsoft.EntityFrameworkCore;
using ProyectoAplicado.Models;
using ProyectoAplicado1.Data;
using ProyectoAplicado1.Models;
using System.Linq.Expressions;

namespace ProyectoAplicado1.Services;

public class ReporteService
{
    private readonly ApplicationDbContext _contexto;
    public ReporteService(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }
    //Metodo Existe 
    public async Task<bool> Existe(int reporteId)
    {
        return await _contexto.Reportes.AnyAsync(c => c.ReporteId == reporteId);
    }

    //Metodo el cual Nos Identifica si ese reporte esta registrado en la base de datos
    public async Task<bool> ExisteNombreReporte(string descripcion, int id)
    {
        return await _contexto.Reportes
            .AnyAsync(c => c.Nombre.ToLower().Equals(descripcion.ToLower()) && c.ReporteId != id);
    }

    //Metodo Insertar
    private async Task<bool> Insertar(Reportes reporte)
    {
        _contexto.Reportes.Add(reporte);
        return await _contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar 
    public async Task<bool> Modificar(Reportes reporte)
    {
        _contexto.Reportes.Update(reporte);
        var modificado = await _contexto.SaveChangesAsync() > 0;
        return modificado;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(Reportes reporte)
    {
        if (!await Existe(reporte.ReporteId))
            return await Insertar(reporte);
        else
            return await Modificar(reporte);
    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int reporteId)
    {
        var eliminado = await _contexto.Reportes
            .Where(c => c.ReporteId == reporteId)
            .ExecuteDeleteAsync();
        return eliminado > 0;
    }
    //Metodo Buscar
    public async Task<Reportes?> Buscar(int reporteId)
    {
        return await _contexto.Reportes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ReporteId == reporteId);
    }
    //Metodo Listar
    public async Task<List<Reportes>> Listar(Expression<Func<Reportes, bool>> criterio)
    {
        return await _contexto.Reportes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ValidarReporteUnico(int reporteId, string nombre, string descripcion)
    {
        var reporteExistente = await _contexto.Reportes
                .Where(c => c.ReporteId != reporteId)
                .Where(c => c.Nombre == nombre || c.Descripcion == descripcion)
                .FirstOrDefaultAsync();

        return reporteExistente == null;
    }
}