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
    public class ReservationsController : ApiControllerBase
    {
        // GET: api/Reservations
        public IQueryable<Reservations> GetReservations()
        {
            return db.Reservations;
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservations))]
        public IHttpActionResult GetReservations(int id)
        {
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }

        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservations(int id, Reservations reservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservations.Id)
            {
                return BadRequest();
            }

            db.Entry(reservations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ReservationsExists(id))
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

        // POST: api/Reservations
        [ResponseType(typeof(Reservations))]
        public IHttpActionResult PostReservations(Reservations reservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reservations.Add(reservations);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reservations.Id }, reservations);
        }

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservations))]
        public IHttpActionResult DeleteReservations(int id)
        {
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservations);
            db.SaveChanges();

            return Ok(reservations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationsExists(int id)
        {
            return db.Reservations.Count(e => e.Id == id) > 0;
        }
    }
}