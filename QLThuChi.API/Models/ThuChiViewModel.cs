﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLThuChi.API.Models
{
    public class ThuChiViewModel
    {
        [Required]
        public int NguoiThuchiId { get; set; }

        [Required]
        public DateTime NgayThuchi { get; set; }

        [Required]
        public int? LydoId { get; set; }

        [Required]
        public double? Tien { get; set; }

        public string GhiChu { get; set; }
    }
}