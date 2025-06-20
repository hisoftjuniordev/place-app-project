// Datoteka: Models/NastavitveObracuna.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
    public class NastavitveObracuna
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime VeljavnoOd { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MinimalnaPlaca { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CenaPrehrane { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CenaPrevozaKm { get; set; }
    }
}