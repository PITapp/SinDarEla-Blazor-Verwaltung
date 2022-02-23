using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinDarElaVerwaltung.Models.DbSinDarEla
{
  [Table("Mitarbeiter")]
  public partial class Mitarbeiter
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MitarbeiterID
    {
      get;
      set;
    }

    public IEnumerable<MitarbeiterFortbildungen> MitarbeiterFortbildungens { get; set; }
    public IEnumerable<MitarbeiterKundenbudget> MitarbeiterKundenbudgets { get; set; }
    public IEnumerable<MitarbeiterTaetigkeiten> MitarbeiterTaetigkeitens { get; set; }
    public IEnumerable<MitarbeiterUrlaub> MitarbeiterUrlaubs { get; set; }
    public IEnumerable<MitarbeiterUrlaubKumuliertAnspruch> MitarbeiterUrlaubKumuliertAnspruches { get; set; }
    public IEnumerable<MitarbeiterUrlaubKumuliertDienstzeiten> MitarbeiterUrlaubKumuliertDienstzeitens { get; set; }
    public IEnumerable<MitarbeiterVerlaufDienstzeiten> MitarbeiterVerlaufDienstzeitens { get; set; }
    public IEnumerable<MitarbeiterVerlaufGehalt> MitarbeiterVerlaufGehalts { get; set; }
    public IEnumerable<MitarbeiterVerlaufNormalarbeitszeit> MitarbeiterVerlaufNormalarbeitszeits { get; set; }
    public int BaseID
    {
      get;
      set;
    }
    public Base Base { get; set; }
    public int MitarbeiterArtID
    {
      get;
      set;
    }
    public MitarbeiterArten MitarbeiterArten { get; set; }
    public int MitarbeiterStatusID
    {
      get;
      set;
    }
    public MitarbeiterStatus MitarbeiterStatus { get; set; }
    public DateTime? ArbeitsrechtEintritt
    {
      get;
      set;
    }
    public DateTime? ArbeitsrechtAustritt
    {
      get;
      set;
    }
    public DateTime? LetzterEintritt
    {
      get;
      set;
    }
    public DateTime? LetzterAustritt
    {
      get;
      set;
    }
    public string Notfallkontakt
    {
      get;
      set;
    }
    public string NotfallkontaktTelefon
    {
      get;
      set;
    }
    public string Info
    {
      get;
      set;
    }
    public string InfoGF
    {
      get;
      set;
    }
  }
}
