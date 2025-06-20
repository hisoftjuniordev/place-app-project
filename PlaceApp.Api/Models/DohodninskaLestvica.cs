// Datoteka: Models/DohodninskaLestvica.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
    public class DohodninskaLestvica
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ZaporednaSt { get; set; } // Npr. 1 za prvi razred, 2 za drugega...

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MejaDo { get; set; } // Mesečna neto davčna osnova do katere sega ta razred

        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Stopnja { get; set; } // Davčna stopnja, npr. 0.16 za 16%

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AbsolutniZnesek { get; set; } // Absolutni znesek davka iz prejšnjih razredov
    }
}