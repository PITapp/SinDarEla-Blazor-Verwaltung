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

  [Route("odata/dbSinDarEla/PersistedGrants")]
  public partial class PersistedGrantsController : ODataController
  {
    private Data.DbSinDarElaContext context;

    public PersistedGrantsController(Data.DbSinDarElaContext context)
    {
      this.context = context;
    }
    // GET /odata/DbSinDarEla/PersistedGrants
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.DbSinDarEla.PersistedGrant> GetPersistedGrants()
    {
      var items = this.context.PersistedGrants.AsQueryable<Models.DbSinDarEla.PersistedGrant>();
      this.OnPersistedGrantsRead(ref items);

      return items;
    }

    partial void OnPersistedGrantsRead(ref IQueryable<Models.DbSinDarEla.PersistedGrant> items);

    partial void OnPersistedGrantGet(ref SingleResult<Models.DbSinDarEla.PersistedGrant> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Key}")]
    public SingleResult<PersistedGrant> GetPersistedGrant(string key)
    {
        var items = this.context.PersistedGrants.Where(i=>i.Key == Uri.UnescapeDataString(key));
        var result = SingleResult.Create(items);

        OnPersistedGrantGet(ref result);

        return result;
    }
    partial void OnPersistedGrantDeleted(Models.DbSinDarEla.PersistedGrant item);
    partial void OnAfterPersistedGrantDeleted(Models.DbSinDarEla.PersistedGrant item);

    [HttpDelete("{Key}")]
    public IActionResult DeletePersistedGrant(string key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.PersistedGrants
                .Where(i => i.Key == Uri.UnescapeDataString(key))
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnPersistedGrantDeleted(item);
            this.context.PersistedGrants.Remove(item);
            this.context.SaveChanges();
            this.OnAfterPersistedGrantDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnPersistedGrantUpdated(Models.DbSinDarEla.PersistedGrant item);
    partial void OnAfterPersistedGrantUpdated(Models.DbSinDarEla.PersistedGrant item);

    [HttpPut("{Key}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutPersistedGrant(string key, [FromBody]Models.DbSinDarEla.PersistedGrant newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.Key != Uri.UnescapeDataString(key)))
            {
                return BadRequest();
            }

            this.OnPersistedGrantUpdated(newItem);
            this.context.PersistedGrants.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.PersistedGrants.Where(i => i.Key == Uri.UnescapeDataString(key));
            this.OnAfterPersistedGrantUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{Key}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchPersistedGrant(string key, [FromBody]Delta<Models.DbSinDarEla.PersistedGrant> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.PersistedGrants.Where(i => i.Key == Uri.UnescapeDataString(key)).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnPersistedGrantUpdated(item);
            this.context.PersistedGrants.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.PersistedGrants.Where(i => i.Key == Uri.UnescapeDataString(key));
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnPersistedGrantCreated(Models.DbSinDarEla.PersistedGrant item);
    partial void OnAfterPersistedGrantCreated(Models.DbSinDarEla.PersistedGrant item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.DbSinDarEla.PersistedGrant item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnPersistedGrantCreated(item);
            this.context.PersistedGrants.Add(item);
            this.context.SaveChanges();

            return Created($"odata/DbSinDarEla/PersistedGrants/{item.Key}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
