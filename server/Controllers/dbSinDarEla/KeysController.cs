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

  [Route("odata/dbSinDarEla/Keys")]
  public partial class KeysController : ODataController
  {
    private Data.DbSinDarElaContext context;

    public KeysController(Data.DbSinDarElaContext context)
    {
      this.context = context;
    }
    // GET /odata/DbSinDarEla/Keys
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.DbSinDarEla.Key> GetKeys()
    {
      var items = this.context.Keys.AsQueryable<Models.DbSinDarEla.Key>();
      this.OnKeysRead(ref items);

      return items;
    }

    partial void OnKeysRead(ref IQueryable<Models.DbSinDarEla.Key> items);

    partial void OnKeyGet(ref SingleResult<Models.DbSinDarEla.Key> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Key> GetKey(string key)
    {
        var items = this.context.Keys.Where(i=>i.Id == Uri.UnescapeDataString(key));
        var result = SingleResult.Create(items);

        OnKeyGet(ref result);

        return result;
    }
    partial void OnKeyDeleted(Models.DbSinDarEla.Key item);
    partial void OnAfterKeyDeleted(Models.DbSinDarEla.Key item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteKey(string key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Keys
                .Where(i => i.Id == Uri.UnescapeDataString(key))
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnKeyDeleted(item);
            this.context.Keys.Remove(item);
            this.context.SaveChanges();
            this.OnAfterKeyDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnKeyUpdated(Models.DbSinDarEla.Key item);
    partial void OnAfterKeyUpdated(Models.DbSinDarEla.Key item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutKey(string key, [FromBody]Models.DbSinDarEla.Key newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.Id != Uri.UnescapeDataString(key)))
            {
                return BadRequest();
            }

            this.OnKeyUpdated(newItem);
            this.context.Keys.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Keys.Where(i => i.Id == Uri.UnescapeDataString(key));
            this.OnAfterKeyUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchKey(string key, [FromBody]Delta<Models.DbSinDarEla.Key> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Keys.Where(i => i.Id == Uri.UnescapeDataString(key)).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnKeyUpdated(item);
            this.context.Keys.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Keys.Where(i => i.Id == Uri.UnescapeDataString(key));
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnKeyCreated(Models.DbSinDarEla.Key item);
    partial void OnAfterKeyCreated(Models.DbSinDarEla.Key item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.DbSinDarEla.Key item)
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

            this.OnKeyCreated(item);
            this.context.Keys.Add(item);
            this.context.SaveChanges();

            return Created($"odata/DbSinDarEla/Keys/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
