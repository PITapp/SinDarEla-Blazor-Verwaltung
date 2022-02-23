using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinDarElaVerwaltung.Models.DbSinDarEla
{
  [Table("vwRollen")]
  public partial class VwRollen
  {
    [Key]
    public string Id
    {
      get;
      set;
    }
    public string Titel
    {
      get;
      set;
    }
  }
}
