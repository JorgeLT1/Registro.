using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PagosBLL {
    private Contexto _contexto;

    public PagosBLL(Contexto contexto) {
        _contexto = contexto;
    }

    public bool Existe(int pagosId) {
        return _contexto.Pagos.Any(o => o.PagosId == pagosId);
    }

    private bool Insertar(Pagos pagos) {
        _contexto.Pagos.Add(pagos);
        return _contexto.SaveChanges() > 0;
    }

    private bool Modificar(Pagos pagos) {
        _contexto.Entry(pagos).State = EntityState.Modified;
        return _contexto.SaveChanges() > 0;
    }

    public bool Guardar(Pagos pagos) {
        if (!Existe(pagos.PagosId))
            return this.Insertar(pagos);
        else
            return this.Modificar(pagos);
    }

    public bool Eliminar(Pagos pagos) {
        _contexto.Entry(pagos).State = EntityState.Deleted;
        return _contexto.SaveChanges() > 0;
    }

    public Pagos ? Buscar(int pagosid) {
        return _contexto.Pagos
            .Where(o => o.PagosId == pagosid)
            .AsNoTracking()
            .SingleOrDefault();
    }

    public List < Persona > GetList() {
        return _contexto.Persona.ToList();
    }

    public List < Ocupaciones > GetLists() {
        return _contexto.Ocupaciones.ToList();
    }

    public List < Pagos > GetListst() {
        return _contexto.Pagos.ToList();
    }

    public List < Prestamos > PrestamosList() {
        return _contexto.Prestamos.ToList();
    }
}