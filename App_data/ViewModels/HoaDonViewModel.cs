using App_data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.ViewModels
{
    public class HoaDonViewModel
    {
    
        public DateTime? NgayThanhToan { get; set; }
        public string? TenNguoiNhan { get; set; }
        public string? SDT { get; set; }
        public string? Email { get; set; }
        public string DiaChi { get; set; }
        public int TienKhachDua { get; set; }
        public long TongTien { get; set; }
        public int TienShip { get; set; }
     
        public string PhuongThucThanhToan { get; set; }
        public Guid IdTrangThaiGiaoHang { get; set; }
        public Guid? IdNguoiDung { get; set; }
        public List<ChiTietHoaDonView> ChiTietHoaDons { get; set; }
    }
}
