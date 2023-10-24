using App_data.ViewModels.ChiTietSPView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.ViewModels
{
    public class AllChiTietSanPham
    {
        public Guid Id { get; set; }
        public string TenAnh { get; set; }
        public string? TenMauSac { get; set; }
        public string? TenSanPham { get; set; }
        public string? TenLoaiSP { get; set; }
        public string? MaKhuyenMai { get; set; }
        public string TenNhaCungCap { get; set; }
        public string? TenSize { get; set; }
        public int GiaGoc { get; set; }
        public double GiaKhuyenMai { get; set; }
        public int soluong { get; set; }
        public int TrangThai { get; set; }
        //public  List<AnhView>? listAnhs { get; set; }

        //public  List<MauSacView>? listMauSacs { get; set; }
        //public  List<SizeView>? listSizes { get; set; }
    }
}
