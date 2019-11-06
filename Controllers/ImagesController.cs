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
    public class ImagesController : ApiControllerBase
    {

        // GET: api/Images
        public IQueryable<Images> GetImages()
        {
            return db.Images;
        }

        // GET: api/Images/5
        [ResponseType(typeof(Images))]
        public IHttpActionResult GetImages(int id)
        {
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return NotFound();
            }

            return Ok(images);
        }

        // PUT: api/Images/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutImages(int id, Images images)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
            var existingPage = db.Images.FirstOrDefault(x => x.Id == images.Id);
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

                existingPage.Pl_Halls_Id = images.Pl_Halls_Id;
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

        // POST: api/Images
        [ResponseType(typeof(Images))]
        public HttpResponseMessage PostImages(Images images)
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
                images.Image_Url = PicturePath;
                // NOTE: To store in memory use postedFile.InputStream
            }
            db.Images.Add(images);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // DELETE: api/Images/5
        [ResponseType(typeof(Images))]
        public IHttpActionResult DeleteImages(int id)
        {
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return NotFound();
            }

            db.Images.Remove(images);
            db.SaveChanges();

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

        private bool ImagesExists(int id)
        {
            return db.Images.Count(e => e.Id == id) > 0;
        }
    }
}