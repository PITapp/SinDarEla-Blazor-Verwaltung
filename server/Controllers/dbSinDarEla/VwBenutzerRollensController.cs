using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;




namespace SinDarElaVerwaltung.Controllers.DbSinDarEla
{
  using Models;
  using Data;
  using Models.DbSinDarEla;

  [Route("odata/dbSinDarEla/VwBenutzerRollens")]
  public partial class VwBenutzerRollensController : ODataController
  {
    private Data.DbSinDarElaContext context;

    public VwBenutzerRollensController(Data.DbSinDarElaContext context)
    {
      this.context = context;
    }
    // GET /odata/DbSinDarEla/VwBenutzerRollens
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.DbSinDarEla.VwBenutzerRollen> GetVwBenutzerRollens()
    {
      var items = this.context.VwBenutzerRollens.AsNoTracking().AsQueryable<Models.DbSinDarEla.VwBenutzerRollen>();
      this.OnVwBenutzerRollensRead(ref items);

      return items;
    }

    partial void OnVwBenutzerRollensRead(ref IQueryable<Models.DbSinDarEla.VwBenutzerRollen> items);

    partial void OnVwBenutzerRollenGet(ref SingleResult<Models.DbSinDarEla.VwBenutzerRollen> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{AspNetUsers_Id}")]
    public SingleResult<VwBenutzerRollen> GetVwBenutzerRollen(string key)
    {
        var items = this.context.VwBenutzerRollens.AsNoTracking().Where(i=>i.AspNetUsers_Id == key);
        var result = SingleResult.Create(items);

        OnVwBenutzerRollenGet(ref result);

        return result;
    }
  }
}
