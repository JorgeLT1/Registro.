using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class OcupacionesBLL {
   private Contexto _contexto;

   public OcupacionesBLL(Contexto contexto) {
      _contexto = contexto;
   }

   public bool Existe(int ocupacionId) {
      return _contexto.Ocupaciones.Any(o => o.OcupacionId == ocupacionId);
   }

   private bool Insertar(Ocupaciones ocupacion) {
      _contexto.Ocupaciones.Add(ocupacion);
      return _contexto.SaveChanges() > 0;
   }

   private bool Modificar(Ocupaciones ocupacion) {
      _contexto.Entry(ocupacion).State = EntityState.Modified;
      return _contexto.SaveChanges() > 0;
   }

public bool Guardar(Ocupaciones ocupacion) {
   if (ocupacion == null || ocupacion.OcupacionId <= 0) {
      return false; // Libro no válido, no se puede guardar
   }

   if (!Existe(ocupacion.OcupacionId)) {
      return this.Insertar(ocupacion);
   } else {
      return this.Modificar(ocupacion);
   }
}

public bool Eliminar(Ocupaciones ocupacion)
{
    if (Existe(ocupacion.OcupacionId))
    {
        var ocupacioneEliminar = _contexto.Ocupaciones.Find(ocupacion.OcupacionId);
        _contexto.Entry(ocupacioneEliminar).State = EntityState.Deleted;
        return _contexto.SaveChanges() > 0;
    }
    else
    {
        return false; 
    }
}

   public Ocupaciones ? Buscar(int ocupacionId) {
      return _contexto.Ocupaciones
         .Where(o => o.OcupacionId == ocupacionId)
         .AsNoTracking()
         .SingleOrDefault();
   }

   public List < Ocupaciones > GetList() {
      return _contexto.Ocupaciones.ToList();
   }

}