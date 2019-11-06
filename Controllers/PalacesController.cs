using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using SarayTap.Attributes;
using SarayTap.Models;

namespace SarayTap.Controllers
{
    [ApiException]
    [RoutePrefix("api/palaces")]
    public class PalacesController : ApiControllerBase
    {
        // GET: api/Palaces
        [Route("")]
        public IQueryable<Palaces> GetPalaces()
        {
            return db.Palaces;
        }

        // GET: api/Palaces/5
        [Route("{id}")]
        [ResponseType(typeof(Palaces))]
        public IHttpActionResult GetPalaces(int id)
        {
            Palaces palaces = db.Palaces.Find(id);
            if (palaces == null)
            {
                return NotFound();
            }

            return Ok(palaces);
        }

        // PUT: api/Palaces/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutPalaces(int id, Palaces palaces)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
            var existingPage = db.Palaces.FirstOrDefault(x => x.Id == palaces.Id);
            if (existingPage == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {


                    var postedFile = httpRequest.Files[file];
                    var name = Guid.NewGuid() + postedFile.FileName;
                    var TeamsPicture = "Public/Images/OurTeams/" + name;
                    var filePath = HttpContext.Current.Server.MapPath("~/" + TeamsPicture);
                    postedFile.SaveAs(filePath);



                    var deletefulpath = "~/" + existingPage.Image_Url;
                    if (File.Exists(deletefulpath))
                    {
                        File.Delete(deletefulpath);
                    }
                    existingPage.Image_Url = TeamsPicture;

                }
                existingPage.Name = palaces.Name;
                existingPage.Latitude = palaces.Latitude;
                existingPage.Longitude = palaces.Longitude;
                existingPage.Location = palaces.Location;
                existingPage.Price_Max = palaces.Price_Max;
                existingPage.Price_Min = palaces.Price_Min;
                db.Entry(existingPage).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {

                    HttpResponseMessage errorResponse = new HttpResponseMessage(HttpStatusCode.BadGateway);
                    errorResponse.ReasonPhrase = e.Message;

                    throw new HttpResponseException(errorResponse);

                }
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }

        // POST: api/Palaces
        [Route("")]
        [ResponseType(typeof(Palaces))]
        public HttpResponseMessage PostPalaces(Palaces palaces)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
            var httpRequest = HttpContext.Current.Request;

            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var name = Guid.NewGuid() + postedFile.FileName;
                var PicturePath = "Public/Images/OurTeams/" + name;
                var filePath = HttpContext.Current.Server.MapPath("~/" + PicturePath);
                postedFile.SaveAs(filePath);
                palaces.Image_Url = PicturePath;
                // NOTE: To store in memory use postedFile.InputStream
            }
            db.Palaces.Add(palaces);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // DELETE: api/Palaces/5
        [Route("{id}")]
        [ResponseType(typeof(Palaces))]
        public IHttpActionResult DeletePalaces(int id)
        {
            Palaces palaces = db.Palaces.Find(id);
            if (palaces == null)
            {
                return NotFound();
            }

            db.Palaces.Remove(palaces);
            db.SaveChanges();

            return Ok(palaces);
        }



        //GET: api/Palaces/id/Pl_Halls
        [Route("{id}/Pl_Halls")]
        public IHttpActionResult GetPalacesPl_Halls(int id)
        {
            Palaces palaces = db.Palaces.Find(id);
            var halls = db.Pl_Halls.Where(x => x.Palace_Id == id).ToList();
            if (palaces == null && halls == null)
            {
                return NotFound();
            }
            return Ok(halls);
        }

        //GET: api/Palaces/id/Pl_Halls/hallsID
        [Route("{id}/Pl_Halls/{hallsID}")]
        public IHttpActionResult GetPalacesPl_halls(int id, int hallsID)
        {
            Palaces palaces = db.Palaces.Find(id);
            var halls = db.Pl_Halls.Where(x => x.Id == hallsID).ToList();
            if (palaces == null && halls == null)
            {
                return NotFound();
            }
            return Ok(halls);
        }
        //GET: api/Palaces/id/Events
        [Route("{id}/Events")]
        public IHttpActionResult GetPalacesEvents(int id)
        {
            Palaces palaces = db.Palaces.Find(id);
            List<Pl_Events> plEvents = db.Pl_Events.Where(x => x.Palace_Id == id).ToList();
            List<Events> events = new List<Events>();
            foreach (var item in plEvents)
            {
                events = db.Events.Where(x => x.Pl_Events == item).ToList();
            }

            if (palaces == null && events == null)
            {
                return NotFound();
            }
            return Ok(events);
        }

        //GET: api/Palaces/id/Events/eventsID
        [Route("{id}/Events/{eventsID}")]
        public IHttpActionResult GetPalacesEvents(int id, int eventsID)
        {
            Palaces palaces = db.Palaces.Find(id);
            var events = db.Events.Where(x => x.Pl_Events.First().Palace_Id == id && x.Id==eventsID).ToList();
            if (palaces == null && events == null)
            {
                return NotFound();
            }
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

        private bool PalacesExists(int id)
        {
            return db.Palaces.Count(e => e.Id == id) > 0;
        }
    }
}