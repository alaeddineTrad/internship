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
    public class CommentsApiController : ApiController
    {
        private RDSContext db = new RDSContext();

        // GET: api/CommentsApi
        public IQueryable<Comment> GetInteractions()
        {
            return (IQueryable <Comment>)db.Interactions;
        }

        // GET: api/CommentsApi/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(long id)
        {
            Comment comment = (Comment)db.Interactions.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/CommentsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(long id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.InteractionId)
            {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/CommentsApi
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Interactions.Add(comment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comment.InteractionId }, comment);
        }

        // DELETE: api/CommentsApi/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(long id)
        {
            Comment comment = (Comment)db.Interactions.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Interactions.Remove(comment);
            db.SaveChanges();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(long id)
        {
            return db.Interactions.Count(e => e.InteractionId == id) > 0;
        }
    }
}