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
    public class Pl_ServicesController : ApiControllerBase
    {
        // GET: api/Pl_Services
        public IQueryable<Pl_Services> GetPl_Services()
        {
            return db.Pl_Services;
        }

        // GET: api/Pl_Services/5
        [ResponseType(typeof(Pl_Services))]
        public IHttpActionResult GetPl_Services(int id)
        {
            Pl_Services pl_Services = db.Pl_Services.Find(id);
            if (pl_Services == null)
            {
                return NotFound();
            }

            return Ok(pl_Services);
        }

        // PUT: api/Pl_Services/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPl_Services(int id, Pl_Services pl_Services)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pl_Services.Id)
            {
                return BadRequest();
            }

            db.Entry(pl_Services).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!Pl_ServicesExists(id))
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

        // POST: api/Pl_Services
        [ResponseType(typeof(Pl_Services))]
        public IHttpActionResult PostPl_Services(Pl_Services pl_Services)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pl_Services.Add(pl_Services);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pl_Services.Id }, pl_Services);
        }

        // DELETE: api/Pl_Services/5
        [ResponseType(typeof(Pl_Services))]
        public IHttpActionResult DeletePl_Services(int id)
        {
            Pl_Services pl_Services = db.Pl_Services.Find(id);
            if (pl_Services == null)
            {
                return NotFound();
            }

            db.Pl_Services.Remove(pl_Services);
            db.SaveChanges();

            return Ok(pl_Services);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Pl_ServicesExists(int id)
        {
            return db.Pl_Services.Count(e => e.Id == id) > 0;
        }
    }
}