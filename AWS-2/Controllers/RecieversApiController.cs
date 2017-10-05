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
    public class RecieversApiController : ApiController
    {
        private RDSContext db = new RDSContext();

        // GET: api/RecieversApi
        public IQueryable<Reciever> GetUsers()
        {

            return (IQueryable < Reciever > )db.Recievers;
        }

        // GET: api/RecieversApi/5
        [ResponseType(typeof(Reciever))]
        public IHttpActionResult GetReciever(long id)
        {
            Reciever reciever = (Reciever) db.Recievers.Find(id);
            if (reciever == null)
            {
                return NotFound();
            }

            return Ok(reciever);
        }

        // PUT: api/RecieversApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReciever(long id, Reciever reciever)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reciever.UserId)
            {
                return BadRequest();
            }

            db.Entry(reciever).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecieverExists(id))
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

        // POST: api/RecieversApi
        [ResponseType(typeof(Reciever))]
        public IHttpActionResult PostReciever(Reciever reciever)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Recievers.Add(reciever);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reciever.UserId }, reciever);
        }

        // DELETE: api/RecieversApi/5
        [ResponseType(typeof(Reciever))]
        public IHttpActionResult DeleteReciever(long id)
        {
            Reciever reciever = (Reciever)db.Users.Find(id);
            if (reciever == null)
            {
                return NotFound();
            }

            db.Recievers.Remove(reciever);
            db.SaveChanges();

            return Ok(reciever);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecieverExists(long id)
        {
            return db.Recievers.Count(e => e.UserId == id) > 0;
        }
    }
}