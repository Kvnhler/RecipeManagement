using System.ComponentModel.DataAnnotations;

namespace RezeptVerwaltung.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Ingredients { get; set; } = string.Empty;
    public int Time { get; set; }
    public string UserId { get; set; } = string.Empty;
    
}