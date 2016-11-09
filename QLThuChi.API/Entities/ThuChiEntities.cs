using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLThuChi.API.Entities
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string Task { get; set; }
        public bool Completed { get; set; }
    }

    public class Lydo
    {
        [Key]
        public int LydoId { get; set; }
        public string TenLydo { get; set; }
        public string Ghichu { get; set; }
        public virtual ICollection<Thuchi> Thuchis { get; set; }
    }

    public class Nguoithuchi
    {
        [Key]
        public int NguoiThuchiId { get; set; }
        public string HoTen { get; set; }
        public string GhiChu { get; set; }

        public virtual ICollection<Thuchi> Thuchis { get; set; }
    }

    public class Thuchi
    {
        [Key]
        public int ThuchiId { get; set; }
        //public int NguoiThuchiId { get; set; }

        //[ForeignKey("NguoiThuchiId")]
        public virtual Nguoithuchi NguoiThuchi { get; set; }
        public DateTime? NgayThuchi { get; set; }
        public bool? KieuThu { get; set; }
        public double? Tien { get; set; }
        //public int? LydoId { get; set; }
        //[ForeignKey("LydoId")]
        public virtual Lydo Lydo { get; set; }
        public string GhiChu { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

}