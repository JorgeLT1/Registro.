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
        InsertarDetalle(pagos);
        _contexto.Pagos.Add(pagos);
        return _contexto.SaveChanges() > 0;
    }

    private bool Modificar(Pagos pagos) {
        ModificarDetalle(pagos);
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
        _contexto.Entry(pago).State = EntityState.Deleted;
        return _contexto.SaveChanges() > 0;
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
public void InsertarDetalle(Pagos pago)
{
    if (pago == null) throw new ArgumentNullException(nameof(pago));

    pago.pagosDetalles ??= new List<PagosDetalle>();

    foreach (var detalle in pago.pagosDetalles)
    {
        var prestamo = _contexto.Prestamos.Find(detalle.prestamosid);
        if (prestamo == null) continue;

        prestamo.balance -= detalle.valorPagado;
        _contexto.Entry(prestamo).State = EntityState.Modified;
    }

    var persona = _contexto.Persona.Find(pago.PersonaId);
    if (persona != null)
    {
        persona.balance -= pago.Monto;
        _contexto.Entry(persona).State = EntityState.Modified;
    }

    _contexto.SaveChanges();
}

public void EliminarDetalle(Pagos pago)
{
    var persona = _contexto.Persona.Find(pago.PersonaId);

    if (persona != null)
    {
        persona.balance += pago.Monto;
        _contexto.Entry(persona).State = EntityState.Modified;
    }

    foreach (var detalle in pago.pagosDetalles)
    {
        var prestamo = _contexto.Prestamos.Find(detalle.prestamosid);

        if (prestamo != null)
        {
            prestamo.balance += detalle.valorPagado;
            _contexto.Entry(prestamo).State = EntityState.Modified;
        }
    }

    _contexto.Entry(pago).State = EntityState.Deleted;
    _contexto.SaveChanges();
}


public void ModificarDetalle(Pagos pagoActual)
{
    var detallesOriginales = _contexto.PagosDetalle
        .AsNoTracking()
        .Where(d => d.PagoId == pagoActual.PagosId)
        .ToList();

    foreach (var detalle in pagoActual.pagosDetalles)
    {
        var prestamo = _contexto.Prestamos.Find(detalle.prestamosid);
        var persona = _contexto.Persona.Find(pagoActual.PersonaId);

        var detalleOriginal = detallesOriginales.FirstOrDefault(d => d.id == detalle.id);
        var esDetalleExistente = detalleOriginal != null;

        if (esDetalleExistente)
        {
            var diferenciaValorPagado = detalleOriginal.valorPagado - detalle.valorPagado;

            if (prestamo != null)
            {
                prestamo.balance += diferenciaValorPagado;
                persona.balance = prestamo.balance;

                _contexto.Entry(prestamo).State = EntityState.Modified;
            }
        }
        else
        {
            if (prestamo != null)
            {
                prestamo.balance -= detalle.valorPagado;
                persona.balance = prestamo.balance;

                _contexto.Entry(prestamo).State = EntityState.Modified;
            }
        }

        _contexto.Entry(persona).State = EntityState.Modified;
        _contexto.SaveChanges();
    }
}


}