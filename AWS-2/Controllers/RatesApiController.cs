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
    public class RatesApiController : ApiController
    {
        private RDSContext db = new RDSContext();

        // GET: api/RatesApi
        public IQueryable<Rate> GetInteractions()
        {
            return db.Rates;
        }

        // GET: api/RatesApi/5
        [ResponseType(typeof(Rate))]
        public IHttpActionResult GetRate(long id)
        {
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return NotFound();
            }

            return Ok(rate);
        }

        // PUT: api/RatesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRate(long id, Rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rate.InteractionId)
            {
                return BadRequest();
            }

            db.Entry(rate).State = EntityState.Modified;

            try
            {
                rate.date = DateTime.Now;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateExists(id))
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

        // POST: api/RatesApi
        [ResponseType(typeof(Rate))]
        public IHttpActionResult PostRate(Rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            rate.date = DateTime.Now;
            db.Rates.Add(rate);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rate.InteractionId }, rate);
        }

        // DELETE: api/RatesApi/5
        [ResponseType(typeof(Rate))]
        public IHttpActionResult DeleteRate(long id)
        {
            Rate rate = (Rate)db.Interactions.Find(id);
            if (rate == null)
            {
                return NotFound();
            }

            db.Interactions.Remove(rate);
            db.SaveChanges();

            return Ok(rate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RateExists(long id)
        {
            return db.Interactions.Count(e => e.InteractionId == id) > 0;
        }
    }
}