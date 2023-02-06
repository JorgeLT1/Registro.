using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PrestamosBLL
{
    private Contexto _contexto;

    public PrestamosBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public bool Existe(int prestamosId)
    {
        return _contexto.Prestamos.Any(o => o.PrestamosId == prestamosId);
    }

    private bool Insertar(Prestamos prestamos)
    {
        _contexto.Prestamos.Add(prestamos);
        return _contexto.SaveChanges() > 0;
    }

    private bool Modificar(Prestamos prestamos)
    {
        _contexto.Entry(prestamos).State = EntityState.Modified;
        return _contexto.SaveChanges() > 0;
    }

    public bool Guardar(Prestamos prestamos)
    {
        if(!Existe(prestamos.PrestamosId))
            return this.Insertar(prestamos);
        else
            return this.Modificar(prestamos);   
    
    }

    public bool Eliminar (Prestamos prestamos)
    {
        _contexto.Entry(prestamos).State = EntityState.Deleted;
        return _contexto.SaveChanges() > 0;
    }

    public Prestamos? Buscar(int prestamosid)
    {
        return _contexto.Prestamos
        .Where(o => o.PrestamosId == prestamosid)
        .AsNoTracking()
        .SingleOrDefault();
    }

    public List<Prestamos> GetList()
    {
        return _contexto.Prestamos.ToList();
    }
    

}
