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
    public class Pl_ManagersController : ApiControllerBase
    {
        // GET: api/Pl_Managers
        public IQueryable<Pl_Managers> GetPl_Managers()
        {
            return db.Pl_Managers;
        }

        // GET: api/Pl_Managers/5
        [ResponseType(typeof(Pl_Managers))]
        public IHttpActionResult GetPl_Managers(int id)
        {
            Pl_Managers pl_Managers = db.Pl_Managers.Find(id);
            if (pl_Managers == null)
            {
                return NotFound();
            }

            return Ok(pl_Managers);
        }

        // PUT: api/Pl_Managers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPl_Managers(int id, Pl_Managers pl_Managers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pl_Managers.Id)
            {
                return BadRequest();
            }

            db.Entry(pl_Managers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!Pl_ManagersExists(id))
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

        // POST: api/Pl_Managers
        [ResponseType(typeof(Pl_Managers))]
        public IHttpActionResult PostPl_Managers(Pl_Managers pl_Managers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pl_Managers.Add(pl_Managers);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pl_Managers.Id }, pl_Managers);
        }

        // DELETE: api/Pl_Managers/5
        [ResponseType(typeof(Pl_Managers))]
        public IHttpActionResult DeletePl_Managers(int id)
        {
            Pl_Managers pl_Managers = db.Pl_Managers.Find(id);
            if (pl_Managers == null)
            {
                return NotFound();
            }

            db.Pl_Managers.Remove(pl_Managers);
            db.SaveChanges();

            return Ok(pl_Managers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Pl_ManagersExists(int id)
        {
            return db.Pl_Managers.Count(e => e.Id == id) > 0;
        }
    }
}