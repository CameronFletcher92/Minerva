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
using System.Web.Http.OData;
using MinervaApi.Models;
using MinervaService;

namespace MinervaService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class EquipmentController : ApiController
    {
        private MinervaContext db = new MinervaContext();

        // GET api/Equipment
        [EnableQuery]
        public IQueryable<Equipment> GetEquipments()
        {
            return db.Equipments;
        }

        // GET api/Equipment/5
        [ResponseType(typeof(Equipment))]
        public IHttpActionResult GetEquipment(long id)
        {
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return NotFound();
            }

            return Ok(equipment);
        }

        // PUT api/Equipment/5
        public IHttpActionResult PutEquipment(long id, Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipment.Id)
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
                if (!EquipmentExists(id))
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

        // POST api/Equipment
        [ResponseType(typeof(Equipment))]
        public IHttpActionResult PostEquipment(Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equipments.Add(equipment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = equipment.Id }, equipment);
        }

        // DELETE api/Equipment/5
        [ResponseType(typeof(Equipment))]
        public IHttpActionResult DeleteEquipment(long id)
        {
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return NotFound();
            }

            db.Equipments.Remove(equipment);
            db.SaveChanges();

            return Ok(equipment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipmentExists(long id)
        {
            return db.Equipments.Count(e => e.Id == id) > 0;
        }
    }
}