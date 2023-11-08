using App_data.Models;
using App_data.ViewModels;
using App_data.ViewModels.ThongKe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly AppDbcontext _dbContext;
        public ThongKeController()
        {
            _dbContext = new AppDbcontext();
        }
        
        // laam them 
        [HttpGet("ThongKeMSSanPhamBan")]
        public List<ThongKeMSSanPhamTheoSoLuong> ThongKeSanPhamMuaSapXep()
        {
            var result = _dbContext.ChiTietHoaDons
                .Join(_dbContext.ChiTietSanPhams, cthd => cthd.IdChiTiepSP, cts => cts.Id, (cthd, cts) => new { ChiTietHoaDon = cthd, ChiTietSanPham = cts })
                .Join(_dbContext.SanPhams, cthd_cts => cthd_cts.ChiTietSanPham.IdSanPham, sp => sp.Id, (cthd_cts, sp) => new { ChiTietHoaDon_ChiTietSanPham = cthd_cts, SanPham = sp })
                
                .Join(_dbContext.HoaDons, cthd_cts_sp_ms => cthd_cts_sp_ms.ChiTietHoaDon_ChiTietSanPham.ChiTietHoaDon.IdHoaDon, hd => hd.Id, (cthd_cts_sp_ms, hd) => new { ChiTietHoaDon_HoaDon = cthd_cts_sp_ms, HoaDon = hd })
                .GroupBy(cthd_cts_sp_ms_hd => cthd_cts_sp_ms_hd.ChiTietHoaDon_HoaDon.SanPham.Id)
                .Select(group => new ThongKeMSSanPhamTheoSoLuong
                {
                    idSanPham = group.FirstOrDefault().ChiTietHoaDon_HoaDon.SanPham.Id,
                    TenSP = group.FirstOrDefault().ChiTietHoaDon_HoaDon.SanPham.Ten,

                    SoLuong = group.Sum(cthd_cts_sp_ms_hd => cthd_cts_sp_ms_hd.ChiTietHoaDon_HoaDon.ChiTietHoaDon_ChiTietSanPham.ChiTietHoaDon.SoLuong),
                    Gia = group.FirstOrDefault().ChiTietHoaDon_HoaDon.ChiTietHoaDon_ChiTietSanPham.ChiTietHoaDon.DonGia,
                    DoanhThu = group.Sum(x=>x.ChiTietHoaDon_HoaDon.ChiTietHoaDon_ChiTietSanPham.ChiTietHoaDon.DonGia* x.ChiTietHoaDon_HoaDon.ChiTietHoaDon_ChiTietSanPham.ChiTietHoaDon.SoLuong),
                    Ngay = group.FirstOrDefault().HoaDon.NgayThanhToan.Value
                })

                .OrderByDescending(item => item.SoLuong).Take(10)
                .ToList();

            return result;
        }
        [HttpGet("ThongKeKHTheoSoLuongHoaDon")]
        public List<ThongKeKHMuaNhieu> ThongKeKHTheoSoLuongHoaDon()
        {
            var result = _dbContext.HoaDons
                .Join(_dbContext.TaiKhoans,hd_kh=>hd_kh.IdNguoiDung,kh=>kh.IdNguoiDung,(hd_kh,kh) => new {HoaDon=hd_kh,KhachHang=kh})
                .
                Where(x => x.HoaDon.NgayThanhToan.HasValue)
                .GroupBy(x => new { x.KhachHang.IdNguoiDung, x.HoaDon.NgayThanhToan.Value.Month })
                .Select(group => new ThongKeKHMuaNhieu
                {

                    idkh = group.FirstOrDefault().KhachHang.IdNguoiDung,
                    Ten = group.FirstOrDefault().KhachHang.Ten,
                    SDT = group.FirstOrDefault().KhachHang.SDT,
                    Email = group.FirstOrDefault().KhachHang.Email,
                    SoDonMua = group.Sum(x => x.HoaDon.Id != null ? 1 : 0),
                    TongSoTien =group.Sum(x=>x.HoaDon.TongTien),
                    Ngay = group.FirstOrDefault().HoaDon.NgayThanhToan.Value
                })

                .OrderByDescending(item => item.TongSoTien).Take(10)
                .ToList();

            return result;

        }
        [HttpGet("ThongKeDoanhThuTheoNgay")]
        public async Task<IActionResult> ThongKeDoanhThuTheoNgay()
        {
            var result = await _dbContext.ChiTietHoaDons
                .Include(ch => ch.HoaDon)
                .Where(ch => ch.HoaDon.NgayThanhToan.HasValue)
                .GroupBy(ch => ch.HoaDon.NgayThanhToan.Value.Date)
                .Select(group => new ThongKeDoanhThu
                {
                    Ngay = group.Key,
                    SoHoaDon = group.Sum(x=>x.HoaDon.Id!=null?1:0),
                    DoanhThu = group.Sum(ch => ch.DonGia * ch.SoLuong + ch.HoaDon.TienShip)
                })
                .OrderByDescending(t => t.Ngay.Date)
                .ToListAsync();

            return Ok(result);
        }
        [HttpGet("LocThongKeDoanhThuTheoNgay")]
        public async Task<IActionResult> LocThongKeDoanhThuTheoNgay(DateTime NgayStart, DateTime NgayEnd)
        {
            if (NgayStart > NgayEnd)
            {
                return null;
            }
            var result = await _dbContext.ChiTietHoaDons
                .Include(ch => ch.HoaDon)
                .Where(ch => ch.HoaDon.NgayThanhToan.HasValue)
                .GroupBy(ch => ch.HoaDon.NgayThanhToan.Value.Date)
                .Select(group => new ThongKeDoanhThu
                {
                    Ngay = group.Key,
                    SoHoaDon = group.Sum(x => x.HoaDon.Id != null ? 1 : 0),
                    DoanhThu = group.Sum(ch => ch.DonGia * ch.SoLuong + ch.HoaDon.TienShip)
                })
                .OrderByDescending(t => t.Ngay.Date)
                .Where(x => x.Ngay.Date >= NgayStart.Date && x.Ngay.Date <= NgayEnd.Date)
                .ToListAsync();
            return Ok(result);
        }
        [HttpGet("ThongKeDoanhThuTheoThang")]
        public List<ThongKeDoanhThu> ThongKeDoanhThuTheoThang()
        {
            var result = _dbContext.ChiTietHoaDons
                .Join(_dbContext.ChiTietSanPhams, cthd => cthd.IdChiTiepSP, cts => cts.Id, (cthd, cts) => new { ChiTietHoaDon = cthd, ChiTietSanPham = cts })
                

                .Join(_dbContext.HoaDons, cthd_cts_sp_ms => cthd_cts_sp_ms.ChiTietHoaDon.IdHoaDon, hd => hd.Id, (cthd_cts_sp_ms, hd) => new { ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac = cthd_cts_sp_ms, HoaDon = hd }).
                Where(x => x.HoaDon.NgayThanhToan.HasValue)
                .GroupBy(cthd_cts_sp_ms_hd => cthd_cts_sp_ms_hd.HoaDon.NgayThanhToan.Value.Month)
                .Select(group => new ThongKeDoanhThu
                {

                    SoHoaDon = group.Sum(x => x.HoaDon.Id != null ? 1 : 0),
                    DoanhThu = group.Sum(x => x.ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac.ChiTietHoaDon.DonGia * x.ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac.ChiTietHoaDon.SoLuong + x.HoaDon.TienShip),
                    Ngay = group.FirstOrDefault().HoaDon.NgayThanhToan.Value
                })

                .OrderByDescending(item => item.Ngay.Month).Take(10)
                .ToList();

            return result;
        }
        [HttpGet("LocThongKeDoanhThuTheoThang")]
        public List<ThongKeDoanhThu> LocThongKeDoanhThuTheoThang(DateTime NgayStart, DateTime NgayEnd)
        {
            if (NgayStart > NgayEnd)
            {
                return null;
            }
            var result = _dbContext.ChiTietHoaDons
                .Join(_dbContext.ChiTietSanPhams, cthd => cthd.IdChiTiepSP, cts => cts.Id, (cthd, cts) => new { ChiTietHoaDon = cthd, ChiTietSanPham = cts })
                

                .Join(_dbContext.HoaDons, cthd_cts_sp_ms => cthd_cts_sp_ms.ChiTietHoaDon.IdHoaDon, hd => hd.Id, (cthd_cts_sp_ms, hd) => new { ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac = cthd_cts_sp_ms, HoaDon = hd })
                .Where(x => x.HoaDon.NgayThanhToan.HasValue)
                .GroupBy(cthd_cts_sp_ms_hd => cthd_cts_sp_ms_hd.HoaDon.NgayThanhToan.Value.Month)
                .Select(group => new ThongKeDoanhThu
                {

                    SoHoaDon = group.Sum(x => x.HoaDon.Id != null ? 1 : 0),
                    DoanhThu = group.Sum(x => x.ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac.ChiTietHoaDon.DonGia * x.ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac.ChiTietHoaDon.SoLuong + x.HoaDon.TienShip),
                    Ngay = group.FirstOrDefault().HoaDon.NgayThanhToan.Value
                })

                .OrderByDescending(item => item.Ngay.Month).Where(x => x.Ngay.Month >= NgayStart.Month && x.Ngay.Month <= NgayEnd.Month).Take(10)
                .ToList();
            return result;
        }
        [HttpGet("ThongKeDoanhThuTheoNam")]
        public List<ThongKeDoanhThu> ThongKeDoanhThuTheoNam()
        {
            var result = _dbContext.ChiTietHoaDons
                .Join(_dbContext.ChiTietSanPhams, cthd => cthd.IdChiTiepSP, cts => cts.Id, (cthd, cts) => new { ChiTietHoaDon = cthd, ChiTietSanPham = cts })
                

                .Join(_dbContext.HoaDons, cthd_cts_sp_ms => cthd_cts_sp_ms.ChiTietHoaDon.IdHoaDon, hd => hd.Id, (cthd_cts_sp_ms, hd) => new { ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac = cthd_cts_sp_ms, HoaDon = hd })
                .Where(x => x.HoaDon.NgayThanhToan.HasValue)
                .GroupBy(cthd_cts_sp_ms_hd => cthd_cts_sp_ms_hd.HoaDon.NgayThanhToan.Value.Year)
                .Select(group => new ThongKeDoanhThu
                {

                    SoHoaDon = group.Sum(x => x.HoaDon.Id != null ? 1 : 0),
                    DoanhThu = group.Sum(x => x.ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac.ChiTietHoaDon.DonGia * x.ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac.ChiTietHoaDon.SoLuong + x.HoaDon.TienShip),
                    Ngay = group.FirstOrDefault().HoaDon.NgayThanhToan.Value
                })

                .OrderByDescending(item => item.Ngay.Year).Take(10)
                .ToList();

            return result;
        }
        [HttpGet("LocThongKeDoanhThuTheoNam")]
        public List<ThongKeDoanhThu> LocThongKeDoanhThuTheoNam(DateTime NgayStart, DateTime NgayEnd)
        {
            if (NgayStart > NgayEnd)
            {
                return null;
            }
            var result = _dbContext.ChiTietHoaDons
                .Join(_dbContext.ChiTietSanPhams, cthd => cthd.IdChiTiepSP, cts => cts.Id, (cthd, cts) => new { ChiTietHoaDon = cthd, ChiTietSanPham = cts })
               

                .Join(_dbContext.HoaDons, cthd_cts_sp_ms => cthd_cts_sp_ms.ChiTietHoaDon.IdHoaDon, hd => hd.Id, (cthd_cts_sp_ms, hd) => new { ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac = cthd_cts_sp_ms, HoaDon = hd })
                .Where(x => x.HoaDon.NgayThanhToan.HasValue)
                .GroupBy(cthd_cts_sp_ms_hd => cthd_cts_sp_ms_hd.HoaDon.NgayThanhToan.Value.Year)
                .Select(group => new ThongKeDoanhThu
                {

                    SoHoaDon = group.Sum(x => x.HoaDon.Id != null ? 1 : 0),
                    DoanhThu = group.Sum(x => x.ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac.ChiTietHoaDon.DonGia * x.ChiTietHoaDon_ChiTietSanPham_SanPham_MauSac.ChiTietHoaDon.SoLuong + x.HoaDon.TienShip),
                    Ngay = group.FirstOrDefault().HoaDon.NgayThanhToan.Value
                })

                .OrderByDescending(item => item.Ngay.Year).Where(x => x.Ngay.Year >= NgayStart.Year && x.Ngay.Year <= NgayEnd.Year).Take(10)
                .ToList();
            return result;
        }



    }
}
