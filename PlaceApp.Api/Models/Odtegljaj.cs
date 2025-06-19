// Datoteka: Models/Odtegljaj.cs
// Opis: Ta razred predstavlja tabelo 'Odtegljaji' v bazi.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
  [Table("Odtegljaji")]
  public class Odtegljaj
  {
    [Key]
    public int ID { get; set; }

    public int ZaposleniID { get; set; }

    [ForeignKey("ZaposleniID")]
    public Zaposlen Zaposlen { get; set; }

    public string Opis { get; set; }

    public decimal SkupniZnesek { get; set; }

    public decimal MesecniObrok { get; set; }

    public DateTime ZacetekOdplacevanja { get; set; }

    public DateTime? KonecOdplacevanja { get; set; }

    public bool JeAktiven { get; set; }
  }
}
