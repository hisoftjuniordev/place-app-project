
// Datoteka: Models/Pogodba.cs
// Opis: Ta razred predstavlja tabelo 'Pogodbe' v bazi.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
  [Table("Pogodbe")]
  public class Pogodba
  {
    [Key]
    public int ID { get; set; }

    public int ZaposleniID { get; set; }

    [ForeignKey("ZaposleniID")]
    public Zaposlen Zaposlen { get; set; }

    public decimal BrutoPlaca { get; set; }

    public DateTime ZacetekVeljavnosti { get; set; }

    public DateTime? KonecVeljavnosti { get; set; }

    public decimal? DodatekMinuloDelo { get; set; }
  }
}

