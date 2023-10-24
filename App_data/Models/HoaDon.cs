using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
   public class HoaDon
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public string? TenNguoiNhan { get; set; }
        public string? SDT { get; set; }
        public string? Email { get; set; }
        public string DiaChi { get; set; }
        public int TienKhachDua { get; set; }
        public long TongTien { get; set; }
        public int TienShip { get; set; }
        public long TienThua { get; set; }   
        public string PhuongThucThanhToan { get; set; }
        public Guid IdTrangThaiGiaoHang { get; set; }
        public Guid? IdNguoiDung { get; set; }
        
       
        public virtual TaiKhoan? TaiKhoans { get; set; }
        public virtual TrangThaiHoaDon? Trangthais { get; set; }
       
        public virtual IEnumerable<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
        public virtual IEnumerable<ChiTietPhuongThucThanhToan>? ChiTietPTTTs { get; set; }
    }
}
