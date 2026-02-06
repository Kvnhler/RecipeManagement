using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RezeptVerwaltung.Data;
using RezeptVerwaltung.Models;

namespace RezeptVerwaltung.Pages.Recipes;

[Authorize]
public class Create : PageModel
{
    private readonly ApplicationDbContext _context;

    public Create(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Recipe NewRecipe { get; set; }
    /*
     * [BindProperty] (Der "Daten-Dolmetscher")
     * Das Attribut [BindProperty] ist wie ein automatischer Dolmetscher.
     * Es gibt dem Framework eine klare Anweisung:
     *"Hey ASP.NET, wenn ein Formular abgeschickt wird, schau dir die Namen der Eingabefelder an.
     * Wenn ein Name zu einer Eigenschaft in meinem NewRecipe passt,
     * dann nimm den Wert und stopfe ihn automatisch dort hinein!"
     */

    /*
     * Was ist IActionResult?
     * Das ist ein Rückgabetyp (ein Interface), der dem Webserver sagt, was er als Nächstes tun soll.
     * "Result" bedeutet Ergebnis, und "Action" bedeutet Aktion.Stell dir vor, du bist am Ende der Methode OnPostAsync.
     * Du musst dem Browser sagen, was jetzt passieren soll.
     * IActionResult ist der Oberbegriff für verschiedene Befehle:
     * Page()"Bleib auf dieser Seite und zeig das HTML an" (z.B. bei einem Tippfehler im Formular).
     * RedirectToPage("./Index")"Geh weg von hier und lade die Übersicht neu" (nach dem Speichern).
     * NotFound()"Diese ID gibt es nicht, zeig eine Fehlerseite".
     */

    public async Task<IActionResult> OnPostAsync()
    {
        //1. User-ID holen
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //2. Dem Rezept eine neue ID zuweisen
        NewRecipe.UserId = userId;

        //3. Das Rezept dem "Bauarbeiter" (Context) geben
        _context.Recipes.Add(NewRecipe);

        //_context merkt sich welche Rezepte als letztes dazu gekommen sind (ein "Noch Nicht erledigt Stapel"), führt eine interne Liste, wenn dann saveChanges aufgerufen wird speichert er alle noch auf dem Stapel liegenden Rezepte in die DB, sehr effizient

        //4.Die Änderungen wirklich in die Datenbank übertragen
        await _context.SaveChangesAsync();

        //5. Dem Browser sagen: "Fertig! Gehe zurück zur Liste"
        return RedirectToPage("./Index");
    }

    /*
     * Server, ich fange jetzt an zu arbeiten.
     * Hier hast du schon mal einen Pager (Task). Sobald ich fertig bin,
     * wird dieser Pager dir ein Endergebnis (IActionResult) liefern, das dir sagt,
     * ob du den User weiterleiten oder ihm eine Fehlermeldung zeigen sollst.
     */
}