using System.ComponentModel.DataAnnotations;

public class PagosDetalle
{
    [Key]

    public int id {get; set;}
    

    [Required (ErrorMessage ="La descripción es requerida")]

    public int? PagoId{get; set;}
    public int? prestamosid{get;set;}
    public double? valorPagado{get;set;}
    
}