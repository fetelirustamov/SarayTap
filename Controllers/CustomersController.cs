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
using SarayTap.Models;

namespace SarayTap.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiControllerBase
    {

        // GET: api/Customers
        [Route("")]
        public IQueryable<Customers> GetCustomers()
        {
            return db.Customers;
        }

        // GET: api/Customers/5
        [Route("{id}")]
        [ResponseType(typeof(Customers))]
        public IHttpActionResult GetCustomers(int id)
        {
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        // PUT: api/Customers/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomers(int id, Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customers.Id)
            {
                return BadRequest();
            }

            db.Entry(customers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
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

        // POST: api/Customers
        [Route("{id}")]
        [ResponseType(typeof(Customers))]
        public IHttpActionResult PostCustomers(Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customers);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customers.Id }, customers);
        }

        // DELETE: api/Customers/5
        [Route("{id}")]
        [ResponseType(typeof(Customers))]
        public IHttpActionResult DeleteCustomers(int id)
        {
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customers);
            db.SaveChanges();

            return Ok(customers);
        }

        //GET: api/Customers/{id}/reservations
        [Route("{id}/reservations")]
        public IHttpActionResult GetCustomerReservations(int id)
        {
            Customers customers = db.Customers.Find(id);
            var reservations = db.Reservations.Where(x => x.Customer_Id == id).ToList();
            if (customers == null && reservations == null)
            {
                return NotFound();
            }
            return Ok(reservations);
        }


        //GET: api/Customers/{id}/reservations{id}
        [Route("{id}/reservations/{reservationID}")]
        public IHttpActionResult GetCustomerReservation(int id,int reservationID)
        {
            Customers customers = db.Customers.Find(id);
            var reservation = db.Reservations.SingleOrDefault(x => x.Id == reservationID);
            if (customers == null && reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomersExists(int id)
        {
            return db.Customers.Count(e => e.Id == id) > 0;
        }

    }
}