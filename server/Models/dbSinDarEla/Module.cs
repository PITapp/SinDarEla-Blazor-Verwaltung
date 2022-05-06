using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinDarElaVerwaltung.Models.DbSinDarEla
{
  [Table("Module")]
  public partial class Module
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ModulID
    {
      get;
      set;
    }

    public IEnumerable<BenutzerModule> BenutzerModules { get; set; }
    public string ModulName
    {
      get;
      set;
    }
    public string Notiz
    {
      get;
      set;
    }
  }
}
