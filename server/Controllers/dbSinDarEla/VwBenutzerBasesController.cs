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

  [Route("odata/dbSinDarEla/VwBenutzerBases")]
  public partial class VwBenutzerBasesController : ODataController
  {
    private Data.DbSinDarElaContext context;

    public VwBenutzerBasesController(Data.DbSinDarElaContext context)
    {
      this.context = context;
    }
    // GET /odata/DbSinDarEla/VwBenutzerBases
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.DbSinDarEla.VwBenutzerBase> GetVwBenutzerBases()
    {
      var items = this.context.VwBenutzerBases.AsNoTracking().AsQueryable<Models.DbSinDarEla.VwBenutzerBase>();
      this.OnVwBenutzerBasesRead(ref items);

      return items;
    }

    partial void OnVwBenutzerBasesRead(ref IQueryable<Models.DbSinDarEla.VwBenutzerBase> items);

    partial void OnVwBenutzerBaseGet(ref SingleResult<Models.DbSinDarEla.VwBenutzerBase> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{AspNetUsers_Id}")]
    public SingleResult<VwBenutzerBase> GetVwBenutzerBase(string key)
    {
        var items = this.context.VwBenutzerBases.AsNoTracking().Where(i=>i.AspNetUsers_Id == key);
        var result = SingleResult.Create(items);

        OnVwBenutzerBaseGet(ref result);

        return result;
    }
  }
}
