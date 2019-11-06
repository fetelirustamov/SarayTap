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
    public class Service_TypesController : ApiControllerBase
    {
        // GET: api/Service_Types
        public IQueryable<Service_Types> GetService_Types()
        {
            return db.Service_Types;
        }

        // GET: api/Service_Types/5
        [ResponseType(typeof(Service_Types))]
        public IHttpActionResult GetService_Types(int id)
        {
            Service_Types service_Types = db.Service_Types.Find(id);
            if (service_Types == null)
            {
                return NotFound();
            }

            return Ok(service_Types);
        }

        // PUT: api/Service_Types/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutService_Types(int id, Service_Types service_Types)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service_Types.Id)
            {
                return BadRequest();
            }

            db.Entry(service_Types).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!Service_TypesExists(id))
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

        // POST: api/Service_Types
        [ResponseType(typeof(Service_Types))]
        public IHttpActionResult PostService_Types(Service_Types service_Types)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Service_Types.Add(service_Types);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = service_Types.Id }, service_Types);
        }

        // DELETE: api/Service_Types/5
        [ResponseType(typeof(Service_Types))]
        public IHttpActionResult DeleteService_Types(int id)
        {
            Service_Types service_Types = db.Service_Types.Find(id);
            if (service_Types == null)
            {
                return NotFound();
            }

            db.Service_Types.Remove(service_Types);
            db.SaveChanges();

            return Ok(service_Types);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Service_TypesExists(int id)
        {
            return db.Service_Types.Count(e => e.Id == id) > 0;
        }
    }
}