using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RezeptVerwaltung.Data;
using RezeptVerwaltung.Models;

namespace RezeptVerwaltung.Pages.Recipes;

[AllowAnonymous]
public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public bool ownerIsLoggedIn { get; set; }
    
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public Recipe Recipe { get; set; }
    
    //Das "int id" kommt aus der URL
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        //Wir suchen das Rezept, das die ID hat UND dem User gehört (Sicherheit!)
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        
        Recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

        
        if (Recipe == null)
        {
        return NotFound();    
        }

        return Page();
    }
    
    // Diese Methode wird durch asp-page-handler="Delete" aufgerufen
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

        if (recipe != null)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
    
    //Diese Methode wird durch asp-page-handler="Edit" aufgerufen
    public async Task<IActionResult> OnPostEditAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page(); //Falls z.B. der Titel leer ist, bleib auf der Seite
        }
        
        //BODYGUARD-LOGIK: Wir holen uns die ECHTE ID des aktuell eingeloggten Users
        var  currentRealUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        
       // Wir überschreiben den (eventuell manipulierten) Wert aus dem Formular
       // mit der sicheren ID aus dem System-Token.
       Recipe.UserId = currentRealUserId;
       
       //Dem Context sagen, dass dieses Objekt existiert und geändert wurde
       _context.Attach(Recipe).State = EntityState.Modified;
       
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            //Falls das Rezept währenddessen gelöscht wurde
            return NotFound();
        }
        
        //TempData ist wie ein Kurzzeitgedächtnis, man legt dort eine Nachricht ab und sie bleibt genau so lange gespeichert, bis die einmal angezeigt wurde. Dachach löscht sier sich selbst.
        //TempData["SuccessMessage"] = "Das Rezept wurde erfolgreich aktualisiert.";
        
        //Nach dem Speichern laden wur die Detils-Seite einfach neu
        return RedirectToPage("Details", new { id = Recipe.Id });
    }
   
}