// Datoteka: Models/ParameterObracuna.cs
// Opis: Ta razred predstavlja tabelo 'ParametriObracuna' v bazi.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
  [Table("ParametriObracuna")]
  public class ParameterObracuna
  {
    [Key]
    public int ID { get; set; }

    public string Naziv { get; set; }

    public string Kljuc { get; set; }

    public string Vrednost { get; set; }

    public DateTime VeljavnoOd { get; set; }

    public DateTime? VeljavnoDo { get; set; }
  }
}
