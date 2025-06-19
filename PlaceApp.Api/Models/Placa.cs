// Datoteka: Models/Placa.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
    public class Placa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ZaposlenId { get; set; }
        [ForeignKey("ZaposlenId")]
        public Zaposlen Zaposlen { get; set; }

        [Required]
        public DateTime Obdobje { get; set; }

        // --- Vnosi iz obrazca ---
        public decimal UrnaPostavka { get; set; }
        public int RednoDeloUre { get; set; }
        public int PlacaniPraznikiUre { get; set; }
        public decimal Poracun { get; set; }
        public decimal Stimulacija { get; set; }
        public decimal DelovnaDobaProcent { get; set; }
        public decimal PosebneOlajsave { get; set; }
        public decimal Odtegljaji { get; set; }
        public int PrehranaDni { get; set; }
        public decimal PrehranaZnesek { get; set; }
        public int PrevozKm { get; set; }
        public decimal KmCena { get; set; }
        public decimal KilometrineFiksno { get; set; }

        // --- Glavni izračunani zneski ---
        public decimal BrutoPlaca { get; set; }
        public decimal NetoPlaca { get; set; }
        public decimal Dohodnina { get; set; }
        public decimal ZaIzplacilo { get; set; }
        public decimal CelotenStrosekDelodajalca { get; set; }
    }
}