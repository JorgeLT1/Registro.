using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pagos
{
    [Key]

    public int PagosId {get; set;}
    

    [Required (ErrorMessage ="La descripción es requerida")]

    public DateTime? Fecha{get; set;}
    public int PersonaId{get;set;}
    public string? Concepto{get;set;}
    public double? Monto{get;set;}

    [ForeignKey("PagosId")]

    public virtual List<PagosDetalle> pagosDetalles {get; set;} = new List<PagosDetalle>();

    
}