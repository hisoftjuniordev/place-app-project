// Datoteka: Models/EvidencaUra.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
    public class EvidencaUra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ZaposlenId { get; set; }
        [ForeignKey("ZaposlenId")]
        public Zaposlen Zaposlen { get; set; }

        [Required]
        public DateTime Datum { get; set; }

        public int RedneUre { get; set; }
        public int Nadure { get; set; }
        public int PraznikUre { get; set; }
        public int DopustUre { get; set; }
        public int BolniskaUre { get; set; }

        // Status bo omogočil kasnejše potrjevanje ur s strani vodje
        [Required]
        public string Status { get; set; } = "Vnešeno"; // Privzeta vrednost

        public string? Opomba { get; set; }
    }
}