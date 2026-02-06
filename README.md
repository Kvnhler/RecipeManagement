# 🍳 RezeptVerwaltung

### *Fullstack .NET Lernprojekt & Wissens-Repository*

Dieses Projekt ist eine moderne Webanwendung auf Basis von **ASP.NET Core 9.0**. Es dient nicht nur der Verwaltung von Rezepten, sondern dokumentiert meinen gesamten Lernprozess beim Meistern des Frameworks.

---

## 🎯 Fokus des Projekts
Ich habe diese Anwendung entwickelt, um die **Best Practices** der Fullstack-Entwicklung zu verstehen. 

> **Besonderheit:** Der Quellcode ist intensiv mit **Lern-Notizen** kommentiert. Diese erklären architektonische Entscheidungen, Sicherheitskonzepte und technische Hürden, die ich während der Entwicklung gemeistert habe.

---

## 🛠 Tech-Stack
* **Backend:** C# / .NET 9.0 (Razor Pages)
* **Datenbank:** Entity Framework Core (EF Core) mit SQLite
* **Security:** ASP.NET Core Identity (Authentifizierung & Autorisierung)
* **Frontend:** Bootstrap 5 (Responsive Design) & Razor Syntax

---

## 🚀 Kern-Funktionen & Konzepte

### 1. 🛡️ Sicherheit (Idiotensicher & Hackerresistent)
* **Ownership-Check:** Verifizierung der `UserId` auf Serverebene. Nur der **echte Besitzer** kann Rezepte bearbeiten oder löschen.
* **Over-Posting Schutz:** Gezieltes Mapping von Properties, um die Manipulation sensibler Daten durch den Client zu unterbinden.
* **Defense in Depth:** UI-Elemente werden dynamisch per Razor ausgeblendet, während die Logik im Code-Behind (`OnPost`) den Zugriff zusätzlich absichert.

### 2. 🔍 Globale Suche
* **Performance:** Einsatz von `IQueryable`-Abfragen, um Daten effizient in der SQL-Datenbank zu filtern.
* **User Experience:** Case-insensitive Suche über Titel und Zutaten direkt aus der Navigationsleiste heraus.

### 3. 📢 Benutzer-Feedback
* **TempData:** Nutzung von transientem Speicher für **Erfolgsbanner** nach dem Speichern oder Löschen.
* **Dynamische UI:** Die Oberfläche passt sich automatisch an (Gast vs. Eingeloggter User vs. Besitzer).

---

## 🧠 Lern-Meilensteine
Folgende Konzepte habe ich in diesem Projekt tiefgreifend verankert:
* **Datenbank-Management:** Umgang mit dem `DbContext`, Migrationen und komplexen LINQ-Abfragen.
* **Error Handling:** Vermeidung von `NullReferenceExceptions` durch saubere Logik-Checks.
* **Lifecycle:** Verständnis von `OnGetAsync` vs. `OnPostAsync` und korrekte Datenbindung (`[BindProperty]`).

---

## Demo



---

## 💻 Installation & Start
1.  **Repository klonen:** `git clone https://github.com/dein-benutzername/RezeptVerwaltung.git`
2.  **Datenbank vorbereiten:** `dotnet ef database update`
3.  **App starten:** `dotnet run`

---

**entwickelt von Kevin Hiller**
