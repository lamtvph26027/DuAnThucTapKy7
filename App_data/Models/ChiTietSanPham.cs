using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
   public class ChiTietSanPham
    {
        [Key]
        public Guid Id { get; set; }
        public int TrangThai { get; set; }
        public Guid IdAnh { get; set; }
        public Guid IdMauSac { get; set; }
        public Guid IdSanPham { get; set; }
        public Guid IdLoaiSP { get; set; }
        public Guid? IdKhuyenMai { get; set; }
        public Guid IdNhaCungCap { get; set; }
        public Guid IdSize { get; set; }
        public int DonGia { get; set; }
        public int soluong { get; set; }
        public  Anh? Anhs { get; set; }
        /// <summary>
        ///  them nhieu thuoc tinh stard
        /// </summary>
        public List<Anh>? listAnhs { get; set; }
        public List<MauSac>? listMauSacs { get; set; }
        public List<Size>? listSizes { get; set; }
        /// <summary>
        /// them nhieu thuoc tinh end
        /// </summary>
        public virtual SanPham?  SanPhams { get; set; }
        public  MauSac? MauSacs { get; set; }
        public virtual NhaCungCap? NhaCungCaps { get; set; }
        public virtual LoaiSP? LoaiSPs { get; set; }
        public  Size? Sizes { get; set; }
        public virtual KhuyenMai? KhuyenMais { get; set; }
        public virtual IEnumerable<DanhGia>? DanhGias { get; set; }
        public virtual IEnumerable<ChiTietGioHang>? ChiTietGioHangs { get; set; }
        public virtual IEnumerable<ChiTietHoaDon>?   ChiTietHoaDons { get; set; }
    }
}
