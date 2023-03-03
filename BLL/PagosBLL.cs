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

   public bool Guardar(Pagos pago) {
   if (pago == null || pago.PagosId <= 0) {
      return false; // Libro no válido, no se puede guardar
   }
   
   if (!Existe(pago.PagosId)) {
      return this.Insertar(pago);
   } else {
      return this.Modificar(pago);
   }
}

public bool Eliminar(Pagos pago)
{
    if (Existe(pago.PagosId))
    {
        var pagosEliminar = _contexto.Pagos.Find(pago.PagosId);
        _contexto.Entry(pagosEliminar).State = EntityState.Deleted;
        return _contexto.SaveChanges() > 0;
    }
    else
    {
        return false; 
    }
}
public bool AgregarDetalles(Pagos pago) {
    if (pago.pagosDetalles == null || pago.pagosDetalles.Count == 0) {
        return false;
    }

    foreach (PagosDetalle detalle in pago.pagosDetalles) {
        detalle.PagoId = pago.PagosId;
        _contexto.PagosDetalle.Add(detalle); // Agregar la entidad al contexto
    }

    return _contexto.SaveChanges() > 0; // Guardar los cambios en la base de datos
}



    public Pagos ? Buscar(int pagosid) {
        return _contexto.Pagos
            .Where(o => o.PagosId == pagosid)
            .AsNoTracking()
            .SingleOrDefault();
    }


    public List<Pagos> GetList(Expression<Func<Pagos, bool>> criterio)
    {
            return _contexto.Pagos.AsNoTracking().Where(criterio).ToList();
    }


}