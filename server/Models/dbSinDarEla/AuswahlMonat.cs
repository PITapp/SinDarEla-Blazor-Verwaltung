using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinDarElaVerwaltung.Models.DbSinDarEla
{
  [Table("AuswahlMonat")]
  public partial class AuswahlMonat
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AuswahlMonatID
    {
      get;
      set;
    }
    public int MonatZahl
    {
      get;
      set;
    }
    public string MonatText
    {
      get;
      set;
    }
  }
}
