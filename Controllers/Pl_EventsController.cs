using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SarayTap.Attributes;
using SarayTap.Models;

namespace SarayTap.Controllers
{
    [ApiException]
    public class Pl_EventsController : ApiControllerBase
    {
        // GET: api/Pl_Events
        public IQueryable<Pl_Events> GetPl_Events()
        {
            return db.Pl_Events;
        }

        // GET: api/Pl_Events/5
        [ResponseType(typeof(Pl_Events))]
        public IHttpActionResult GetPl_Events(int id)
        {
            Pl_Events pl_Events = db.Pl_Events.Find(id);
            if (pl_Events == null)
            {
                return NotFound();
            }

            return Ok(pl_Events);
        }

        // PUT: api/Pl_Events/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPl_Events(int id, Pl_Events pl_Events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pl_Events.Id)
            {
                return BadRequest();
            }

            db.Entry(pl_Events).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!Pl_EventsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    HttpResponseMessage errorResponse = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                    errorResponse.ReasonPhrase = e.Message;
                    throw new HttpResponseException(errorResponse);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Pl_Events
        [ResponseType(typeof(Pl_Events))]
        public IHttpActionResult PostPl_Events(Pl_Events pl_Events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pl_Events.Add(pl_Events);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pl_Events.Id }, pl_Events);
        }

        // DELETE: api/Pl_Events/5
        [ResponseType(typeof(Pl_Events))]
        public IHttpActionResult DeletePl_Events(int id)
        {
            Pl_Events pl_Events = db.Pl_Events.Find(id);
            if (pl_Events == null)
            {
                return NotFound();
            }

            db.Pl_Events.Remove(pl_Events);
            db.SaveChanges();

            return Ok(pl_Events);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Pl_EventsExists(int id)
        {
            return db.Pl_Events.Count(e => e.Id == id) > 0;
        }
    }
}