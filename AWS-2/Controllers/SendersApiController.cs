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
    public class SendersApiController : ApiController
    {
        private RDSContext db = new RDSContext();

        // GET: api/SendersApi
        public IQueryable<Sender> GetUsers()
        {
            return (IQueryable<Sender>)db.Senders;
        }

        // GET: api/SendersApi/5
        [ResponseType(typeof(Sender))]
        public IHttpActionResult GetSender(long id)
        {
            Sender sender =(Sender)db.Senders.Find(id);
            if (sender == null)
            {
                return NotFound();
            }

            return Ok(sender);
        }

        // PUT: api/SendersApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSender(long id, Sender sender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sender.UserId)
            {
                return BadRequest();
            }

            db.Entry(sender).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SenderExists(id))
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

        // POST: api/SendersApi
        [ResponseType(typeof(Sender))]
        public IHttpActionResult PostSender(Sender sender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Senders.Add(sender);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sender.UserId }, sender);
        }

        // DELETE: api/SendersApi/5
        [ResponseType(typeof(Sender))]
        public IHttpActionResult DeleteSender(long id)
        {
            Sender sender = (Sender)db.Senders.Find(id);
            if (sender == null)
            {
                return NotFound();
            }

            db.Senders.Remove(sender);
            db.SaveChanges();

            return Ok(sender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SenderExists(long id)
        {
            return db.Senders.Count(e => e.UserId == id) > 0;
        }
    }
}