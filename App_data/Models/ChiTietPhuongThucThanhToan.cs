using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
    public class ChiTietPhuongThucThanhToan
    {
        public Guid Id { get; set; }
        public Guid IdPTTT { get; set; }
        public Guid IdHoaDon { get; set; }
        public int SoTien { get; set; }
        public int TrangThai { get; set; }
        public virtual HoaDon HoaDons { get; set; }
        public virtual PhuongThucThanhToan PhuongThucThanhToans { get; set; }

    }
}
