
using System.ComponentModel.DataAnnotations;

public class Prestamos
{
        [Key]
    public int PrestamosId {get; set;}
    
    [Required (ErrorMessage ="La descripción es requerida")]

    public DateTime? fecha{get; set;}
    public DateTime? vence{get; set;}
    public string? concepto{get;set;}
    public long? monto{get;set;}
    public int personaid{get;set;}
    public double balance {get; set;}
}