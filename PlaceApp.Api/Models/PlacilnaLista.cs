using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
  [Table("PlacilneListe")]
  public class PlacilnaLista
  {
    [Key]
    public int ID { get; set; }

    public int ObracunID { get; set; }

    [ForeignKey("ObracunID")]
    public Obracun Obracun { get; set; }

    public int ZaposleniID { get; set; }

    [ForeignKey("ZaposleniID")]
    public Zaposlen Zaposlen { get; set; }

    public decimal SteviloUr { get; set; }

    public decimal UreOdsotnosti { get; set; }

    public decimal SkupajBruto { get; set; }

    public decimal NetoZaIzplacilo { get; set; }
  }
}

