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
    [RoutePrefix("api/Nguoithuchi")]
    public class NguoithuchisController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Nguoithuchis
        [Authorize]
        [Route("")]
        public IQueryable<Nguoithuchi> GetNguoithuchis()
        {
            return db.Nguoithuchis;
        }
        [Authorize]
        [Route("{id}")]
        [HttpGet]
        // GET: api/Nguoithuchis/5
        [ResponseType(typeof(Nguoithuchi))]
        public async Task<IHttpActionResult> GetNguoithuchi(int id)
        {
            Nguoithuchi nguoithuchi = await db.Nguoithuchis.FindAsync(id);
            if (nguoithuchi == null)
            {
                return NotFound();
            }

            return Ok(nguoithuchi);
        }

        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        [HttpPut]
        // PUT: api/Nguoithuchis/5
        [ResponseType(typeof(Nguoithuchi))]
        public async Task<IHttpActionResult> PutNguoithuchi(int id, Nguoithuchi nguoithuchi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nguoithuchi.NguoiThuchiId)
            {
                return BadRequest();
            }

            db.Entry(nguoithuchi).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                return Ok(nguoithuchi);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguoithuchiExists(id))
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

        // POST: api/Nguoithuchis
        [Authorize(Roles = "Admin")]
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(Nguoithuchi))]
        public async Task<IHttpActionResult> PostNguoithuchi(Nguoithuchi nguoithuchi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Nguoithuchis.Add(nguoithuchi);
            await db.SaveChangesAsync();

            return Ok(nguoithuchi);
        }

        // DELETE: api/Nguoithuchis/5
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        [HttpDelete]
        [ResponseType(typeof(Nguoithuchi))]
        public async Task<IHttpActionResult> DeleteNguoithuchi(int id)
        {
            Nguoithuchi nguoithuchi = await db.Nguoithuchis.FindAsync(id);
            if (nguoithuchi == null)
            {
                return NotFound();
            }

            db.Nguoithuchis.Remove(nguoithuchi);
            await db.SaveChangesAsync();

            return Ok(nguoithuchi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NguoithuchiExists(int id)
        {
            return db.Nguoithuchis.Count(e => e.NguoiThuchiId == id) > 0;
        }
    }
}