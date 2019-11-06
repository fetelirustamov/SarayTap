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
    public class StatusesController : ApiControllerBase
    {
        // GET: api/Statuses
        public IQueryable<Statuses> GetStatuses()
        {
            return db.Statuses;
        }

        // GET: api/Statuses/5
        [ResponseType(typeof(Statuses))]
        public IHttpActionResult GetStatuses(int id)
        {
            Statuses statuses = db.Statuses.Find(id);
            if (statuses == null)
            {
                return NotFound();
            }

            return Ok(statuses);
        }

        // PUT: api/Statuses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStatuses(int id, Statuses statuses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statuses.Id)
            {
                return BadRequest();
            }

            db.Entry(statuses).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!StatusesExists(id))
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

        // POST: api/Statuses
        [ResponseType(typeof(Statuses))]
        public IHttpActionResult PostStatuses(Statuses statuses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Statuses.Add(statuses);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = statuses.Id }, statuses);
        }

        // DELETE: api/Statuses/5
        [ResponseType(typeof(Statuses))]
        public IHttpActionResult DeleteStatuses(int id)
        {
            Statuses statuses = db.Statuses.Find(id);
            if (statuses == null)
            {
                return NotFound();
            }

            db.Statuses.Remove(statuses);
            db.SaveChanges();

            return Ok(statuses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatusesExists(int id)
        {
            return db.Statuses.Count(e => e.Id == id) > 0;
        }
    }
}