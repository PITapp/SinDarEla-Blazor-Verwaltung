using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinDarElaVerwaltung.Models.DbSinDarEla
{
  [Table("vwBaseOrte")]
  public partial class VwBaseOrte
  {
    [Key]
    public string Ort
    {
      get;
      set;
    }
  }
}
