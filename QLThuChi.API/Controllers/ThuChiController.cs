using QLThuChi.API.Entities;
using QLThuChi.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace QLThuChi.API.Controllers
{
    [RoutePrefix("api/Thuchi")]
    public class ThuChiController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Lydoes
        [Authorize]
        [Route("")]
        public IQueryable<Thuchi> Get()
        {
            return db.Thuchis.Where(p => p.User == CurentUser);
        }

        [Authorize]
        [Route("get/{thang}")]
        public IEnumerable<Thuchi> Get(string thang)
        {
            return db.Thuchis.ToList().Where(p => p.NgayThuchi.Value.ToString("yyyyMM") == thang).OrderByDescending(p => p.NgayThuchi);
        }

        [Authorize]
        [Route("{id}")]
        public IHttpActionResult GetThuchi(int id)
        {
            Thuchi Thuchi = Get().SingleOrDefault();
            if (Thuchi == null)
            {
                return NotFound();
            }

            return Ok(Thuchi);
        }

        [Authorize]
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Insert(ThuChiViewModel thuchi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Thuchis.Add(new Thuchi()
            {
                NguoiThuchiId = thuchi.NguoiThuchiId,
                Tien = thuchi.KieuThu ? thuchi.Tien : -thuchi.Tien,
                KieuThu = thuchi.KieuThu,
                LydoId = thuchi.LydoId,
                GhiChu = thuchi.GhiChu,
                NgayThuchi = DateTime.Now,
                UserId = CurentUser.Id
            });
            await db.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, ThuChiViewModel thuchi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tc = db.Thuchis.Where(p => p.LydoId == id).SingleOrDefault();
            tc.NguoiThuchiId = thuchi.NguoiThuchiId;
            tc.Tien = thuchi.KieuThu ? thuchi.Tien : -thuchi.Tien;
            tc.KieuThu = thuchi.KieuThu;
            tc.LydoId = thuchi.LydoId;
            tc.GhiChu = thuchi.GhiChu;
            tc.NgayThuchi = DateTime.Now;
            tc.UserId = CurentUser.Id;
            db.Entry(tc).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThuChiExists(id))
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

        [Authorize]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Thuchi thuchi = await db.Thuchis.FindAsync(id);
            if (thuchi == null)
            {
                return NotFound();
            }

            db.Thuchis.Remove(thuchi);
            await db.SaveChangesAsync();

            return Ok();
        }

        private bool ThuChiExists(int id)
        {
            return db.Thuchis.Count(e => e.ThuchiId == id) > 0;
        }
    }
}
