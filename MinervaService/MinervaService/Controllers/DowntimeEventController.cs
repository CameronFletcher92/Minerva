using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using MinervaApi.Models;
using MinervaService;

namespace MinervaService.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using MinervaApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<DowntimeEvent>("DowntimeEvent");
    builder.EntitySet<Equipment>("Equipments"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DowntimeEventController : ODataController
    {
        private MinervaContext db = new MinervaContext();

        // GET odata/DowntimeEvent
        [EnableQuery]
        public IQueryable<DowntimeEvent> GetDowntimeEvent()
        {
            return db.DowntimeEvents;
        }

        // GET odata/DowntimeEvent(5)
        [EnableQuery]
        public SingleResult<DowntimeEvent> GetDowntimeEvent([FromODataUri] long key)
        {
            return SingleResult.Create(db.DowntimeEvents.Where(downtimeevent => downtimeevent.Id == key));
        }

        // PUT odata/DowntimeEvent(5)
        public IHttpActionResult Put([FromODataUri] long key, DowntimeEvent downtimeevent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != downtimeevent.Id)
            {
                return BadRequest();
            }

            db.Entry(downtimeevent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DowntimeEventExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(downtimeevent);
        }

        // POST odata/DowntimeEvent
        public IHttpActionResult Post(DowntimeEvent downtimeevent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DowntimeEvents.Add(downtimeevent);
            db.SaveChanges();

            return Created(downtimeevent);
        }

        // PATCH odata/DowntimeEvent(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] long key, Delta<DowntimeEvent> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DowntimeEvent downtimeevent = db.DowntimeEvents.Find(key);
            if (downtimeevent == null)
            {
                return NotFound();
            }

            patch.Patch(downtimeevent);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DowntimeEventExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(downtimeevent);
        }

        // DELETE odata/DowntimeEvent(5)
        public IHttpActionResult Delete([FromODataUri] long key)
        {
            DowntimeEvent downtimeevent = db.DowntimeEvents.Find(key);
            if (downtimeevent == null)
            {
                return NotFound();
            }

            db.DowntimeEvents.Remove(downtimeevent);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/DowntimeEvent(5)/Equipment
        [EnableQuery]
        public SingleResult<Equipment> GetEquipment([FromODataUri] long key)
        {
            return SingleResult.Create(db.DowntimeEvents.Where(m => m.Id == key).Select(m => m.Equipment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DowntimeEventExists(long key)
        {
            return db.DowntimeEvents.Count(e => e.Id == key) > 0;
        }
    }
}
