using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
   public class ChiTietHoaDon
    {
        [Key]
        public Guid Id { get; set; }
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        public int TrangThai { get; set; }
        public Guid IdChiTiepSP { get; set; }
        public Guid IdHoaDon { get; set; }
        public virtual HoaDon HoaDon { get; set; }
        public virtual ChiTietSanPham ChiTietSanPham { get; set; }
    }
}
