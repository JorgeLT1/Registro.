using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PagosDetalleBLL {
    private Contexto _contexto;

    public PagosDetalleBLL(Contexto contexto) {
        _contexto = contexto;
    }

    public bool Existe(int pagosDetalles) {
        return _contexto.PagosDetalle.Any(o => o.id == pagosDetalles);
    }

    private bool Insertar(PagosDetalle pagosDetalle) {
        _contexto.PagosDetalle.Add(pagosDetalle);
        return _contexto.SaveChanges() > 0;
    }

    private bool Modificar(PagosDetalle pagosDetalle) {
        _contexto.Entry(pagosDetalle).State = EntityState.Modified;
        return _contexto.SaveChanges() > 0;
    }

    public bool Guardar(PagosDetalle pagosDetalle) {
        if (!Existe(pagosDetalle.id))
            return this.Insertar(pagosDetalle);
        else
            return this.Modificar(pagosDetalle);
    }

    public bool Eliminar(PagosDetalle pagosDetalle) {
        _contexto.Entry(pagosDetalle).State = EntityState.Deleted;
        return _contexto.SaveChanges() > 0;
    }

    public PagosDetalle ? Buscar(int id) {
        return _contexto.PagosDetalle
            .Where(o => o.id == id)
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
    public List < PagosDetalle > PagosDetalleList() {
        return _contexto.PagosDetalle.ToList();
    }

}