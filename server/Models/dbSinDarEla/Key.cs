using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinDarElaVerwaltung.Models.DbSinDarEla
{
  [Table("Keys")]
  public partial class Key
  {
    [Key]
    public string Id
    {
      get;
      set;
    }
    public int Version
    {
      get;
      set;
    }
    public DateTime Created
    {
      get;
      set;
    }
    public string Use
    {
      get;
      set;
    }
    public string Algorithm
    {
      get;
      set;
    }
    public bool IsX509Certificate
    {
      get;
      set;
    }
    public bool DataProtected
    {
      get;
      set;
    }
    public string Data
    {
      get;
      set;
    }
  }
}
