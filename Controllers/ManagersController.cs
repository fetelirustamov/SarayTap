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
    public class ManagersController : ApiControllerBase
    {
        // GET: api/Managers
        public IQueryable<Managers> GetManagers()
        {
            return db.Managers;
        }

        // GET: api/Managers/5
        [ResponseType(typeof(Managers))]
        public IHttpActionResult GetManagers(int id)
        {
            Managers managers = db.Managers.Find(id);
            if (managers == null)
            {
                return NotFound();
            }

            return Ok(managers);
        }

        // PUT: api/Managers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutManagers(int id, Managers managers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != managers.Id)
            {
                return BadRequest();
            }

            db.Entry(managers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ManagersExists(id))
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

        // POST: api/Managers
        [ResponseType(typeof(Managers))]
        public IHttpActionResult PostManagers(Managers managers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Managers.Add(managers);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = managers.Id }, managers);
        }

        // DELETE: api/Managers/5
        [ResponseType(typeof(Managers))]
        public IHttpActionResult DeleteManagers(int id)
        {
            Managers managers = db.Managers.Find(id);
            if (managers == null)
            {
                return NotFound();
            }

            db.Managers.Remove(managers);
            db.SaveChanges();

            return Ok(managers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ManagersExists(int id)
        {
            return db.Managers.Count(e => e.Id == id) > 0;
        }
    }
}