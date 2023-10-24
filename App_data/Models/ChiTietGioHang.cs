using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
   public class ChiTietGioHang
    {
        public Guid Id { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
        public Guid IdChiTietSP { get; set; }
        public Guid IdNguoiDung { get; set; }
        public virtual ChiTietSanPham ChiTietSanPhams { get; set; }
        public virtual GioHang GioHang { get; set; }
    }
}
