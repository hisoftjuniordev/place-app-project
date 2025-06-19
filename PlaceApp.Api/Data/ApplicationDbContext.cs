// Datoteka: Data/ApplicationDbContext.cs
// Opis: To je glavni razred, ki upravlja povezavo z bazo podatkov
// in definira, katere tabele so del našega podatkovnega modela.

using Microsoft.EntityFrameworkCore;
using PlaceApp.Api.Models; // Uvozimo mapo, kjer so naši modeli

namespace PlaceApp.Api.Data
{
  public class ApplicationDbContext : DbContext
  {
    // Konstruktor, ki omogoča, da aplikacija posreduje nastavitve povezave
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Tukaj definiramo vse tabele, s katerimi želimo delati.
    // Ime lastnosti (npr. Podjetja) bomo uporabljali v C# kodi za dostop do tabele.

    public DbSet<Podjetje> Podjetja { get; set; }
    public DbSet<Zaposlen> Zaposleni { get; set; }
    public DbSet<Pogodba> Pogodbe { get; set; }
    public DbSet<Odtegljaj> Odtegljaji { get; set; }
    public DbSet<ParameterObracuna> ParametriObracuna { get; set; }
    public DbSet<Obracun> Obracuni { get; set; }
    public DbSet<PlacilnaLista> PlacilneListe { get; set; }
    public DbSet<PostavkaPlacilneListe> PostavkePlacilneListe { get; set; }

    // Ta metoda se uporabi za dodatne konfiguracije modelov, če so potrebne.
    // Zaenkrat jo lahko pustimo prazno.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}
