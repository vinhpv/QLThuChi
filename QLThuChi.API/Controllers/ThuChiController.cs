using QLThuChi.API.Entities;
using QLThuChi.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
            var a = await db.Thuchis.Where(p => p.NgayThuchi.Value.Year == _nam && p.NgayThuchi.Value.Month == _thang).OrderByDescending(p => p.NgayThuchi)
                .Select(p => new
                {
                    p.ThuchiId,p.Tien,p.LydoId,p.NguoithuchiId,p.NgayThuchi, p.GhiChu,
                    p.Lydo.TenLydo, p.Lydo.KieuThu, p.NguoiThuchi.HoTen, p.UserId, p.User.UserName
                })
                .ToListAsync();
            return Ok(a);
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
            try
            {
                var db2 = new ApplicationDbContext();
                var lydo = db2.Lydoes.Find(thuchi.LydoId);
                var tc = new Thuchi()
                {
                    NguoithuchiId = thuchi.NguoiThuchiId,
                    Tien = lydo.KieuThu == Kieuthu.Thu ? Math.Abs(thuchi.Tien.Value) : -Math.Abs(thuchi.Tien.Value),
                    LydoId = thuchi.LydoId.Value,
                    GhiChu = thuchi.GhiChu,
                    NgayThuchi = thuchi.NgayThuchi,
                    UserId = CurentUser.Id
                };
                db.Thuchis.Add(tc);
                await db.SaveChangesAsync();

                return Ok();
            }
            catch (DbEntityValidationException ex)
            {
                //return BadRequest();

                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                return BadRequest(sb.ToString());

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

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
            var db2 = new ApplicationDbContext();
            var lydo = db2.Lydoes.Find(thuchi.LydoId);

            var tc = db.Thuchis.Where(p => p.ThuchiId == id).SingleOrDefault();
            tc.NguoithuchiId = thuchi.NguoiThuchiId;
            tc.Tien = lydo.KieuThu == Kieuthu.Thu ? Math.Abs(thuchi.Tien.Value) : -Math.Abs(thuchi.Tien.Value);
            tc.LydoId = thuchi.LydoId.Value;
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
