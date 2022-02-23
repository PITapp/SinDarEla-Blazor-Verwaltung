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

  [Route("odata/dbSinDarEla/VwRollens")]
  public partial class VwRollensController : ODataController
  {
    private Data.DbSinDarElaContext context;

    public VwRollensController(Data.DbSinDarElaContext context)
    {
      this.context = context;
    }
    // GET /odata/DbSinDarEla/VwRollens
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.DbSinDarEla.VwRollen> GetVwRollens()
    {
      var items = this.context.VwRollens.AsNoTracking().AsQueryable<Models.DbSinDarEla.VwRollen>();
      this.OnVwRollensRead(ref items);

      return items;
    }

    partial void OnVwRollensRead(ref IQueryable<Models.DbSinDarEla.VwRollen> items);

    partial void OnVwRollenGet(ref SingleResult<Models.DbSinDarEla.VwRollen> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<VwRollen> GetVwRollen(string key)
    {
        var items = this.context.VwRollens.AsNoTracking().Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnVwRollenGet(ref result);

        return result;
    }
  }
}
