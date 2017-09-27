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
    public class InteractionsApiController : ApiController
    {
        private RDSContext db = new RDSContext();

        // GET: api/InteractionsApi
        public IQueryable<Interaction> GetInteractions()
        {
            return db.Interactions;
        }

        // GET: api/InteractionsApi/5
        [ResponseType(typeof(Interaction))]
        public IHttpActionResult GetInteraction(long id)
        {
            Interaction interaction = db.Interactions.Find(id);
            if (interaction == null)
            {
                return NotFound();
            }

            return Ok(interaction);
        }

        // PUT: api/InteractionsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInteraction(long id, Interaction interaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != interaction.InteractionId)
            {
                return BadRequest();
            }
            interaction.date = DateTime.Now;
            db.Entry(interaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InteractionExists(id))
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

        // POST: api/InteractionsApi
        [ResponseType(typeof(Interaction))]
        public IHttpActionResult PostInteraction(Interaction interaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            interaction.date = DateTime.Now;
            db.Interactions.Add(interaction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = interaction.InteractionId }, interaction);
        }

        // DELETE: api/InteractionsApi/5
        [ResponseType(typeof(Interaction))]
        public IHttpActionResult DeleteInteraction(long id)
        {
            Interaction interaction = db.Interactions.Find(id);
            if (interaction == null)
            {
                return NotFound();
            }

            db.Interactions.Remove(interaction);
            db.SaveChanges();

            return Ok(interaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InteractionExists(long id)
        {
            return db.Interactions.Count(e => e.InteractionId == id) > 0;
        }
    }
}