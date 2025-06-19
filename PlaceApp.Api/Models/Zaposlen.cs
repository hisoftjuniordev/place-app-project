using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
  [Table("Zaposleni")]
  public class Zaposlen
  {
    [Key]
    public int ID { get; set; }

    public int PodjetjeID { get; set; }

    [ForeignKey("PodjetjeID")] // Povemo, kako je povezan s tabelo Podjetja
    public Podjetje Podjetje { get; set; }

    [Required]
    public string Ime { get; set; }

    [Required]
    public string Priimek { get; set; }

    [Required]
    public string EMSO { get; set; }

    [Required]
    public string DavcnaStevilka { get; set; }

    public string? Naslov { get; set; }

    [Required]
    public string TRR { get; set; }

    public DateTime DatumZaposlitve { get; set; }

    public DateTime? DatumPrenehanja { get; set; }

    public bool JeAktiven { get; set; }
  }
}
