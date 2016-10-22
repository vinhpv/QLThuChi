using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using QLThuChi.API.Entities;
using QLThuChi.API.Models;

namespace QLThuChi.API.Controllers
{
    public class LydoesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Lydoes
        [Authorize]
        public IQueryable<Lydo> GetLydoes()
        {
            return db.Lydoes;
        }

        // GET: api/Lydoes/5
        [Authorize]
        [ResponseType(typeof(Lydo))]
        public async Task<IHttpActionResult> GetLydo(int id)
        {
            Lydo lydo = await db.Lydoes.FindAsync(id);
            if (lydo == null)
            {
                return NotFound();
            }

            return Ok(lydo);
        }

        [Authorize(Roles ="Admin")]
        // PUT: api/Lydoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLydo(int id, Lydo lydo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lydo.LydoId)
            {
                return BadRequest();
            }

            db.Entry(lydo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LydoExists(id))
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

        [Authorize(Roles = "Admin")]
        // POST: api/Lydoes
        [ResponseType(typeof(Lydo))]
        public async Task<IHttpActionResult> PostLydo(Lydo lydo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lydoes.Add(lydo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lydo.LydoId }, lydo);
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/Lydoes/5
        [ResponseType(typeof(Lydo))]
        public async Task<IHttpActionResult> DeleteLydo(int id)
        {
            Lydo lydo = await db.Lydoes.FindAsync(id);
            if (lydo == null)
            {
                return NotFound();
            }

            db.Lydoes.Remove(lydo);
            await db.SaveChangesAsync();

            return Ok(lydo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LydoExists(int id)
        {
            return db.Lydoes.Count(e => e.LydoId == id) > 0;
        }
    }
}