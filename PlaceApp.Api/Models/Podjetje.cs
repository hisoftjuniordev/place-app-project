using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceApp.Api.Models
{
  [Table("Podjetja")] // Povemo, da ta razred pripada tabeli 'Podjetja'
  public class Podjetje
  {
    [Key] // Označimo, da je 'ID' primarni ključ
    public int ID { get; set; }

    [Required] // Označimo, da je polje obvezno
    public string Naziv { get; set; }

    [Required]
    public string DavcnaStevilka { get; set; }

    [Required]
    public string MaticnaStevilka { get; set; }

    public string? Naslov { get; set; } // vprašaj pomeni, da polje ni obvezno (je lahko NULL)
  }
}

