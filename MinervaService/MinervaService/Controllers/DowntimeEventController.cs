using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using MinervaApi.Models;
using MinervaService;

namespace MinervaService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class DowntimeEventController : ApiController
    {
        private MinervaContext db = new MinervaContext();

        // GET api/DowntimeEvent
        public IQueryable<DowntimeEvent> GetDowntimeEvents()
        {
            return db.DowntimeEvents;
        }

        // GET api/DowntimeEvent/5
        [ResponseType(typeof(DowntimeEvent))]
        public IHttpActionResult GetDowntimeEvent(long id)
        {
            DowntimeEvent downtimeevent = db.DowntimeEvents.Find(id);
            if (downtimeevent == null)
            {
                return NotFound();
            }

            return Ok(downtimeevent);
        }

        // PUT api/DowntimeEvent/5
        public IHttpActionResult PutDowntimeEvent(long id, DowntimeEvent downtimeevent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != downtimeevent.Id)
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
                if (!DowntimeEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/DowntimeEvent
        [ResponseType(typeof(DowntimeEvent))]
        public IHttpActionResult PostDowntimeEvent(DowntimeEvent downtimeevent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DowntimeEvents.Add(downtimeevent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = downtimeevent.Id }, downtimeevent);
        }

        // DELETE api/DowntimeEvent/5
        [ResponseType(typeof(DowntimeEvent))]
        public IHttpActionResult DeleteDowntimeEvent(long id)
        {
            DowntimeEvent downtimeevent = db.DowntimeEvents.Find(id);
            if (downtimeevent == null)
            {
                return NotFound();
            }

            db.DowntimeEvents.Remove(downtimeevent);
            db.SaveChanges();

            return Ok(downtimeevent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DowntimeEventExists(long id)
        {
            return db.DowntimeEvents.Count(e => e.Id == id) > 0;
        }
    }
}