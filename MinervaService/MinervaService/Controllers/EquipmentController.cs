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
    builder.EntitySet<Equipment>("Equipment");
    builder.EntitySet<DowntimeEvent>("DowntimeEvent"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EquipmentController : ODataController
    {
        private MinervaContext db = new MinervaContext();

        // GET odata/Equipment
        [Queryable]
        public IQueryable<Equipment> GetEquipment()
        {
            return db.Equipments;
        }

        // GET odata/Equipment(5)
        [Queryable]
        public SingleResult<Equipment> GetEquipment([FromODataUri] long key)
        {
            return SingleResult.Create(db.Equipments.Where(equipment => equipment.Id == key));
        }

        // PUT odata/Equipment(5)
        public IHttpActionResult Put([FromODataUri] long key, Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != equipment.Id)
            {
                return BadRequest();
            }

            db.Entry(equipment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(equipment);
        }

        // POST odata/Equipment
        public IHttpActionResult Post(Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equipments.Add(equipment);
            db.SaveChanges();

            return Created(equipment);
        }

        // PATCH odata/Equipment(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] long key, Delta<Equipment> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Equipment equipment = db.Equipments.Find(key);
            if (equipment == null)
            {
                return NotFound();
            }

            patch.Patch(equipment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(equipment);
        }

        // DELETE odata/Equipment(5)
        public IHttpActionResult Delete([FromODataUri] long key)
        {
            Equipment equipment = db.Equipments.Find(key);
            if (equipment == null)
            {
                return NotFound();
            }

            db.Equipments.Remove(equipment);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Equipment(5)/DowntimeEvents
        [Queryable]
        public IQueryable<DowntimeEvent> GetDowntimeEvents([FromODataUri] long key)
        {
            return db.Equipments.Where(m => m.Id == key).SelectMany(m => m.DowntimeEvents);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipmentExists(long key)
        {
            return db.Equipments.Count(e => e.Id == key) > 0;
        }
    }
}
