// Datoteka: Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using PlaceApp.Api.Models;

namespace PlaceApp.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Zaposlen> Zaposleni { get; set; }
        public DbSet<Podjetje> Podjetja { get; set; }
        public DbSet<Placa> Place { get; set; }
        public DbSet<Odtegljaj> Odtegljaji { get; set; }
        public DbSet<PlacilnaLista> PlacilneListe { get; set; }
        public DbSet<Pogodba> Pogodbe { get; set; }
        public DbSet<PostavkaPlacilneListe> PostavkePlacilneListe { get; set; }


        // --- DODAJTE TO METODO ZA KONFIGURACIJO MODELA ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nastavitve za entiteto Placa
            modelBuilder.Entity<Placa>(entity =>
            {
                // Za zneske v evrih običajno uporabimo natančnost (18, 2)
                entity.Property(p => p.UrnaPostavka).HasPrecision(18, 4); // Urna postavka je lahko bolj natančna
                entity.Property(p => p.Poracun).HasPrecision(18, 2);
                entity.Property(p => p.Stimulacija).HasPrecision(18, 2);
                entity.Property(p => p.PosebneOlajsave).HasPrecision(18, 2);
                entity.Property(p => p.Odtegljaji).HasPrecision(18, 2);
                entity.Property(p => p.PrehranaZnesek).HasPrecision(18, 2);
                entity.Property(p => p.KmCena).HasPrecision(18, 4); // Cena na km je lahko bolj natančna
                entity.Property(p => p.KilometrineFiksno).HasPrecision(18, 2);
                entity.Property(p => p.BrutoPlaca).HasPrecision(18, 2);
                entity.Property(p => p.NetoPlaca).HasPrecision(18, 2);
                entity.Property(p => p.Dohodnina).HasPrecision(18, 2);
                entity.Property(p => p.ZaIzplacilo).HasPrecision(18, 2);
                entity.Property(p => p.CelotenStrosekDelodajalca).HasPrecision(18, 2);
                entity.Property(p => p.DelovnaDobaProcent).HasPrecision(18, 4);
            });

            // Nastavitve za ostale entitete, ki so se pojavile v opozorilih
            modelBuilder.Entity<Odtegljaj>(entity =>
            {
                entity.Property(p => p.MesecniObrok).HasPrecision(18, 2);
                entity.Property(p => p.SkupniZnesek).HasPrecision(18, 2);
            });

            modelBuilder.Entity<PlacilnaLista>(entity =>
            {
                entity.Property(p => p.NetoZaIzplacilo).HasPrecision(18, 2);
                entity.Property(p => p.SkupajBruto).HasPrecision(18, 2);
                entity.Property(p => p.SteviloUr).HasPrecision(18, 2);
                entity.Property(p => p.UreOdsotnosti).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Pogodba>(entity =>
            {
                entity.Property(p => p.BrutoPlaca).HasPrecision(18, 2);
                entity.Property(p => p.DodatekMinuloDelo).HasPrecision(18, 2);
            });

            modelBuilder.Entity<PostavkaPlacilneListe>(entity =>
            {
                entity.Property(p => p.Znesek).HasPrecision(18, 2);
            });

            // === DODAJTE TA BLOK ZA PREKINITEV CIKLA ===
            // Pravilo dodamo na relacijo, ki jo je javila napaka: FK_PlacilneListe_Zaposleni_ZaposleniID
            modelBuilder.Entity<PlacilnaLista>()
                .HasOne(pl => pl.Zaposlen) // PlacilnaLista ima enega Zaposlenega
                .WithMany() // Ne definiramo povratne navigacije, če je ni
                .HasForeignKey(pl => pl.ZaposleniID) // Tuji ključ
                .OnDelete(DeleteBehavior.Restrict); // KLJUČNA VRSTICA: Prepreči kaskadno brisanje
        }
    }
    }
