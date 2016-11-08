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
#if !DEBUG
        [Authorize]
#endif 
        [Route("get/{thang}")]
        public IEnumerable<Thuchi> Get(string thang)
        {
            return db.Thuchis.ToList().Where(p => p.NgayThuchi.Value.ToString("yyyyMM") == thang).OrderByDescending(p => p.NgayThuchi);
        }

#if !DEBUG
        [Authorize]
#endif
        [Route("Thang/{thang}")]
        public async Task<IHttpActionResult> GetThang(string thang)
        {
            int _nam = int.Parse(thang.Substring(0, 4));
            int _thang = int.Parse(thang.Substring(4, 2));
            return Ok(await db.Thuchis.Where(p => p.NgayThuchi.Value.Year == _nam && p.NgayThuchi.Value.Month == _thang).OrderByDescending(p => p.NgayThuchi).ToListAsync());
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

#if !DEBUG
        [Authorize]
#endif
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
                NguoiThuchi  = db.Nguoithuchis.Find(thuchi.NguoiThuchiId),
                Tien = thuchi.KieuThu ? thuchi.Tien : -thuchi.Tien,
                KieuThu = thuchi.KieuThu,
                Lydo = db.Lydoes.Find(thuchi.LydoId),
                GhiChu = thuchi.GhiChu,
                NgayThuchi = thuchi.NgayThuchi,
                UserId = CurentUser.Id
            });
            await db.SaveChangesAsync();

            return Ok();
        }

#if !DEBUG
        [Authorize]
#endif
        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, ThuChiViewModel thuchi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tc = db.Thuchis.Where(p => p.ThuchiId == id).SingleOrDefault();
            tc.NguoiThuchi = db.Nguoithuchis.Find(thuchi.NguoiThuchiId);
            tc.Tien = thuchi.KieuThu ? thuchi.Tien : -thuchi.Tien;
            tc.KieuThu = thuchi.KieuThu;
            tc.Lydo = db.Lydoes.Find(thuchi.LydoId);
            tc.GhiChu = thuchi.GhiChu;
            tc.NgayThuchi = thuchi.NgayThuchi;
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

#if !DEBUG
        [Authorize]
#endif
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
