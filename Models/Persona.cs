
using System.ComponentModel.DataAnnotations;

public class Persona
{
    [Key]

    public int PersonaId {get; set;}

    [Required (ErrorMessage ="La descripción es requerida")]

    public string? Nombres {get; set;}
    public string? direccion {get; set;}
    public string? email {get; set;}
    public DateTime? nacimiento{get; set;}
    public string? telefono{get;set;}
    public double? balance{get;set;}

    public int? ocupacionId{get;set;}
    
}