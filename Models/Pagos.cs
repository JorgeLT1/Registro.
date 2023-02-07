using System.ComponentModel.DataAnnotations;

public class Pagos
{
    [Key]

    public int PagosId {get; set;}
    

    [Required (ErrorMessage ="La descripción es requerida")]

    public DateTime? Fecha{get; set;}
    public string? PersonaId{get;set;}
    public string? Concepto{get;set;}
    public int? Monto{get;set;}
    
}