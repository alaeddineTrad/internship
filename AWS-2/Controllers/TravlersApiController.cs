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
using AWS_2.Models;

namespace AWS_2.Controllers
{
    public class TravlersApiController : ApiController
    {
        private RDSContext db = new RDSContext();

        // GET: api/TravlersApi
        public IQueryable<Travler> GetUsers()
        {
            return(IQueryable < Travler >) db.Travlers;
        }

        // GET: api/TravlersApi/5
        [ResponseType(typeof(Travler))]
        public IHttpActionResult GetTravler(long id)
        {
            Travler travler = (Travler) db.Travlers.Find(id);
            if (travler == null)
            {
                return NotFound();
            }

            return Ok(travler);
        }

        // PUT: api/TravlersApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTravler(long id, Travler travler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != travler.UserId)
            {
                return BadRequest();
            }

            db.Entry(travler).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravlerExists(id))
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

        // POST: api/TravlersApi
        [ResponseType(typeof(Travler))]
        public IHttpActionResult PostTravler(Travler travler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Travlers.Add(travler);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = travler.UserId }, travler);
        }

        // DELETE: api/TravlersApi/5
        [ResponseType(typeof(Travler))]
        public IHttpActionResult DeleteTravler(long id)
        {
            Travler travler = (Travler)db.Travlers.Find(id);
            if (travler == null)
            {
                return NotFound();
            }

            db.Travlers.Remove(travler);
            db.SaveChanges();

            return Ok(travler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TravlerExists(long id)
        {
            return db.Travlers.Count(e => e.UserId == id) > 0;
        }
    }
}