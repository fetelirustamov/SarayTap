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
    [RoutePrefix("api/Pl_Halls")]
    public class Pl_HallsController : ApiControllerBase
    {

        // GET: api/Pl_Halls
        [Route("")]
        public IQueryable<Pl_Halls> GetPl_Halls()
        {
            return db.Pl_Halls;
        }

        // GET: api/Pl_Halls/5
        [Route("{id}")]
        [ResponseType(typeof(Pl_Halls))]
        public IHttpActionResult GetPl_Halls(int id)
        {
            Pl_Halls pl_Halls = db.Pl_Halls.Find(id);
            if (pl_Halls == null)
            {
                return NotFound();
            }

            return Ok(pl_Halls);
        }

        // PUT: api/Pl_Halls/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPl_Halls(int id, Pl_Halls pl_Halls)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pl_Halls.Id)
            {
                return BadRequest();
            }

            db.Entry(pl_Halls).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!Pl_HallsExists(id))
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

        // POST: api/Pl_Halls
        [Route("")]
        [ResponseType(typeof(Pl_Halls))]
        public IHttpActionResult PostPl_Halls(Pl_Halls pl_Halls)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pl_Halls.Add(pl_Halls);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pl_Halls.Id }, pl_Halls);
        }

        // DELETE: api/Pl_Halls/5
        [Route("{id}")]
        [ResponseType(typeof(Pl_Halls))]
        public IHttpActionResult DeletePl_Halls(int id)
        {
            Pl_Halls pl_Halls = db.Pl_Halls.Find(id);
            if (pl_Halls == null)
            {
                return NotFound();
            }

            db.Pl_Halls.Remove(pl_Halls);
            db.SaveChanges();

            return Ok(pl_Halls);
        }


        //GET: api/Pl_halls/{id}/images/
        [Route("{id}/images")]
        public IHttpActionResult GetPl_HallsImages(int id)
        {
            Pl_Halls halls = db.Pl_Halls.Find(id);
            var images = db.Images.Where(x => x.Pl_Halls_Id == id).ToList();
            if (halls == null && images == null)
            {
                return NotFound();
            }
            return Ok(images);
        }

      
        //GET: api/Pl_halls/{id}/images/{id}
        [Route("{id}/images/{imageID}")]
        public IHttpActionResult GetPl_HallsImages(int id,int imageID)
        {
            Pl_Halls halls = db.Pl_Halls.Find(id);
            Images images = db.Images.SingleOrDefault(x => x.Id == imageID);
            if (halls == null && images == null)
            {
                return NotFound();
            }
            return Ok(images);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Pl_HallsExists(int id)
        {
            return db.Pl_Halls.Count(e => e.Id == id) > 0;
        }
    }
}