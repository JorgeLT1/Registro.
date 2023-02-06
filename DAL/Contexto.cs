
using Microsoft.EntityFrameworkCore;

public class Contexto : DbContext
{
    public DbSet<Ocupaciones> Ocupaciones{get; set;}
    public DbSet<Persona> Persona {get; set;}
    public DbSet<Prestamos> Prestamos{get; set;}

    
    public Contexto(DbContextOptions <Contexto> options): base(options)
    {

    }
}