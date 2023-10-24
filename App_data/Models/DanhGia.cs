using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
   public class DanhGia
    {
        public Guid Id { get; set; }
        public string BinhLuan { get; set; }
        public Guid IdNguoiDung { get; set; }
        public Guid IdChiTietSP { get; set; }
        [Range(1,5,ErrorMessage ="so luong phai trong khoang 1-5")]
        public int Sao { get; set; }
        public int TrangThai { get; set; }
        public virtual ChiTietSanPham ChiTietSanPhams { get; set; }
        public virtual TaiKhoan TaiKhoans { get; set; }
    }
}
