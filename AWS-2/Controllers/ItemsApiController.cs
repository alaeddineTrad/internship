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
    public class ItemsApiController : ApiController
    {
        private RDSContext db = new RDSContext();

        // GET: api/ItemsApi
        public IQueryable<Item> GetObjects()
        {
            return db.Objects;
        }

        // GET: api/ItemsApi/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(long id)
        {
            Item item = db.Objects.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/ItemsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(long id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/ItemsApi
        [ResponseType(typeof(Item))]
        public IHttpActionResult PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Objects.Add(item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        // DELETE: api/ItemsApi/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteItem(long id)
        {
            Item item = db.Objects.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Objects.Remove(item);
            db.SaveChanges();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(long id)
        {
            return db.Objects.Count(e => e.Id == id) > 0;
        }
    }
}