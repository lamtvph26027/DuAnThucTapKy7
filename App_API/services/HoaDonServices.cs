using App_API.Iservices;
using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using App_data.ViewModels;

namespace App_API.services
{
    public class HoaDonServices : IHoaDonServices
    {
        private readonly IAllRepositories<HoaDon> hoadons;
        private readonly IAllRepositories<ChiTietHoaDon> cthoadons;
        private readonly IAllRepositories<ChiTietSanPham> chitietsps;
        AppDbcontext context = new AppDbcontext();
        public HoaDonServices()
        {
            hoadons = new AllRepositories<HoaDon>(context, context.HoaDons);
            cthoadons = new AllRepositories<ChiTietHoaDon>(context, context.ChiTietHoaDons);
            chitietsps = new AllRepositories<ChiTietSanPham>(context, context.ChiTietSanPhams);

        }
        public bool CreateHoaDon(List<ChiTietHoaDonView> chitiethoadons, DateTime? NgayThanhToan, string? TenNguoiNhan, string? SDT, string? Email, string DiaChi, int TienKhachDua, long TongTien, int TienShip, string PhuongThucThanhToan, Guid IdTrangThai, Guid? IdNguoiDung)
        {
            try
            {
                HoaDon hoadon = new HoaDon();
                hoadon.Id=Guid.NewGuid();
                hoadon.TenNguoiNhan=TenNguoiNhan;
                hoadon.NgayTao=DateTime.Now;
                hoadon.NgayThanhToan = NgayThanhToan;
                if (hoadon.NgayThanhToan < hoadon.NgayTao)
                {
                    return false;
                }

                hoadon.SDT=SDT;
                hoadon.Email=Email;
                hoadon.DiaChi=DiaChi;
                hoadon.TienKhachDua=TienKhachDua;
                hoadon.TongTien=TongTien;
                hoadon.TienShip = TienShip;
                hoadon.TienThua = hoadon.TienKhachDua - hoadon.TienShip - hoadon.TongTien;
                hoadon.PhuongThucThanhToan=PhuongThucThanhToan;
                hoadon.IdTrangThaiGiaoHang = IdTrangThai;
                hoadon.IdNguoiDung=IdNguoiDung;
                if (hoadons.Add(hoadon))
                {
                    foreach(var x in chitiethoadons)
                    {
                        ChiTietHoaDon ct = new ChiTietHoaDon();
                        ct.Id= Guid.NewGuid();
                        ct.IdHoaDon = hoadon.Id;
                        ct.IdChiTiepSP=x.IdChiTiepSP;
                        ct.SoLuong=x.SoLuong;
                        ct.DonGia = chitietsps.GetAll().First(y => y.Id == x.IdChiTiepSP).DonGia;
                        ct.TrangThai = 1;
                        cthoadons.Add(ct);
                    }
                }
                return true;

            }
            catch
            {
                return false;
            }

                //hoadon.Id = Guid.NewGuid();
                //hoadon.NgayTao = DateTime.Now;
                //hoadon.NgayThanhToan = NgayThanhToan;
                //if (hoadon.NgayThanhToan < hoadon.NgayTao)
                //{
                //    return false;
                //}
                //hoadon.TenNguoiNhan = ten;
                //hoadon.SDT = SDT;
                //hoadon.Email = email;
                //hoadon.DiaChi = DiaChi;
                //hoadon.TienShip = tienShip;
                //hoadon.PhuongThucThanhToan = phuongThucThanhToan;
                //hoadon.IdTrangThaiGiaoHang = IdTrangthai;
                //hoadon.IdNguoiDung = idNguoiDung;
                //if (hoadons.Add(hoadon))
                //{
                //    foreach (var x in chiTietHoaDons)
                //    {
                //        ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                //        chiTietHoaDon.Id = Guid.NewGuid();
                //        chiTietHoaDon.IdHoaDon = hoadon.Id;
                //        chiTietHoaDon.IdChiTiepSP = x.IdChiTiepSP;
                //        chiTietHoaDon.SoLuong = x.SoLuong;
                //        chiTietHoaDon.DonGia = chitietsps.GetAll().First(y => y.Id == x.IdChiTiepSP).DonGia;
                //        chiTietHoaDon.TrangThai = 1;
                //        cthoadons.Add(chiTietHoaDon);
                //    }
                //}
                //return true;
            }

        public List<HoaDon> GetAllHoaDon()
        {
            return hoadons.GetAll();
        }

        public HoaDon GetHoaDon(Guid id)
        {
            return hoadons.GetAll().FirstOrDefault(x => x.Id == id);
        }

        public bool UpdateTrangThaiGiaoHang(Guid id,Guid IdtrangThai)
        {
            var hoadon = hoadons.GetAll().Where(x => x.Id == id).FirstOrDefault();
            if (hoadon != null)
            {
                hoadon.IdTrangThaiGiaoHang = IdtrangThai;
                return hoadons.Update(hoadon);
            }
            else
            {
                return false;
            }
        }
    }
}
