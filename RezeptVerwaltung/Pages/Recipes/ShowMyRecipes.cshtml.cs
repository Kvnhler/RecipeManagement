using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RezeptVerwaltung.Data;
using RezeptVerwaltung.Models;

namespace RezeptVerwaltung.Pages.Recipes;

public class ShowMyRecipesModel : PageModel
{
    // Dependency Injection
    // Besteht aus 3 verschiedenen Komponenten
    // 1. Das Feld (zugänglich für die gesamte Klasse)
    private readonly ApplicationDbContext _context;

    // 2. Der Konstruktor, der zur Objekterstellung das Objekt von ApplicationDbContext braucht
    public ShowMyRecipesModel(ApplicationDbContext context)
    {
        // 3. setzen der Konstante
        _context = context;
    }

    // Hier sollen dann später die Objekte aus der Db in diese Liste geladen werden
    public List<Recipe> Recipes { get; set; } = new();

    public async void OnGetAsync(string? searchTerm)
    {
        ViewData["CurrentFilter"] = searchTerm;
        
        // 1. Wir definieren die "Basis-Abfrage" (noch wird nichts geladen!)
        var RecipeQuery = from r in _context.Recipes
            select r;
        
        // 2. WENN ein Suchanfrage da ist, hängnen wir einen Filter dran
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            RecipeQuery = RecipeQuery.Where(s => s.Title.Contains(searchTerm) || s.Ingredients.Contains(searchTerm));
        }
        
        // 3. ERST JETZT wird die DB wirklich gefragt (ToListAsync)
        Recipes = await RecipeQuery.ToListAsync();
        
        // In diesem Codeteil werden dann schließlich die Rezepte geladen...

        // Schritt 1: Die ID des aktuell eingeloggten Users holen
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        // vorgelesen macht die Zeile das: "Nimm den aktuellen Benutzer, durchsuche seinen Ausweis nach der Information vom Typ 'Eindeutige ID' und gib mir den Inhalt davon zurück." 
        
        if (userId != null)// schutz falls die Seite ohne eingeloggten User aufgerufen wird
        {
            // Schritt 2: Filtern und in die Liste schreiben
            Recipes = await _context.Recipes // hole die Liste aller Rezepte aus der DB
                .Where(r => r.UserId == userId) // Wo das Attribut UserId in der Liste den selben wert hat wie userId, nehme das Objekt 
                .ToListAsync(); // mache alle aussortierten Rezepte zu einer Liste und überschreibe Recipies mit diesem Wert
        }
    }
}