using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RezeptVerwaltung.Models;

namespace RezeptVerwaltung.Data;
//Diese Klasse ist das Bindeglied zwischen Datenbank und C# Code und übergibt das Model Recipes welches anschließend in eine Tabelle in SQL umgewandelt wird
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<Recipe> Recipes { get; set; }
}