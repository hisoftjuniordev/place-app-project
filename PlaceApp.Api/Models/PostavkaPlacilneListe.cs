// Datoteka: Models/PostavkaPlacilneListe.cs
// Opis: Ta razred predstavlja tabelo 'PostavkePlacilneListe' v bazi.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
  [Table("PostavkePlacilneListe")]
  public class PostavkaPlacilneListe
  {
    [Key]
    public int ID { get; set; }

    public int PlacilnaListaID { get; set; }

    [ForeignKey("PlacilnaListaID")]
    public PlacilnaLista PlacilnaLista { get; set; }

    public string Opis { get; set; }

    public string TipPostavke { get; set; }

    public decimal Znesek { get; set; }
  }
}
