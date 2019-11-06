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
    public class RolsController : ApiControllerBase
    {

        // GET: api/Rols
        public IQueryable<Rols> GetRols()
        {
            return db.Rols;
        }

        // GET: api/Rols/5
        [ResponseType(typeof(Rols))]
        public IHttpActionResult GetRols(int id)
        {
            Rols rols = db.Rols.Find(id);
            if (rols == null)
            {
                return NotFound();
            }

            return Ok(rols);
        }

        // PUT: api/Rols/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRols(int id, Rols rols)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rols.Id)
            {
                return BadRequest();
            }

            db.Entry(rols).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!RolsExists(id))
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

        // POST: api/Rols
        [ResponseType(typeof(Rols))]
        public IHttpActionResult PostRols(Rols rols)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rols.Add(rols);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rols.Id }, rols);
        }

        // DELETE: api/Rols/5
        [ResponseType(typeof(Rols))]
        public IHttpActionResult DeleteRols(int id)
        {
            Rols rols = db.Rols.Find(id);
            if (rols == null)
            {
                return NotFound();
            }

            db.Rols.Remove(rols);
            db.SaveChanges();

            return Ok(rols);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RolsExists(int id)
        {
            return db.Rols.Count(e => e.Id == id) > 0;
        }
    }
}