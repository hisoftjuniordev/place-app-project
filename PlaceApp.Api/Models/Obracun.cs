using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
  [Table("Obracuni")]
  public class Obracun
  {
    [Key]
    public int ID { get; set; }

    public int PodjetjeID { get; set; }

    [ForeignKey("PodjetjeID")]
    public Podjetje Podjetje { get; set; }

    public int Mesec { get; set; }

    public int Leto { get; set; }

    [Required]
    public string Status { get; set; }

    public DateTime DatumIzdelave { get; set; }
  }
}
