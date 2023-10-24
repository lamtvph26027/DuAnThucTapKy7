using App_data.Models;
using App_data.ViewModels;

namespace App_API.Iservices
{
    public interface IHoaDonServices
    {
        public bool CreateHoaDon(List<ChiTietHoaDonView> chitiethoadons,DateTime? NgayThanhToan,string?  TenNguoiNhan,string? SDT,string? Email,string DiaChi,int TienKhachDua,long TongTien,int TienShip,string PhuongThucThanhToan,Guid IdTrangThai,Guid? IdNguoiDung);
        public List<HoaDon> GetAllHoaDon();
        public HoaDon GetHoaDon(Guid id);
       
        public bool UpdateTrangThaiGiaoHang(Guid id,Guid  IdtrangThai);
    }
}
