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
    [RoutePrefix("api/Events")]
    public class EventsController : ApiControllerBase
    {
        // GET: api/Events
        [Route("")]
        public IQueryable<Events> GetEvents()
        {
            return db.Events;
        }

        // GET: api/Events/5
        [Route("{id}")]
        [ResponseType(typeof(Events))]
        public IHttpActionResult GetEvents(int id)
        {
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }

        // PUT: api/Events/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvents(int id, Events events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != events.Id)
            {
                return BadRequest();
            }

            db.Entry(events).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!EventsExists(id))
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

        // POST: api/Events
        [ResponseType(typeof(Events))]

        [Route("")]
        public IHttpActionResult PostEvents(Events events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Events.Add(events);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = events.Id }, events);
        }

        // DELETE: api/Events/5
        [Route("{id}")]
        [ResponseType(typeof(Events))]
        public IHttpActionResult DeleteEvents(int id)
        {
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return NotFound();
            }

            db.Events.Remove(events);
            db.SaveChanges();

            return Ok(events);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventsExists(int id)
        {
            return db.Events.Count(e => e.Id == id) > 0;
        }
    }
}