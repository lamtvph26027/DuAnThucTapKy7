using App_API.Iservices;
using App_API.services;
using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using App_data.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IAllRepositories<HoaDon> hoadons;
        private readonly IAllRepositories<ChiTietHoaDon> cthoadons;
        private readonly IAllRepositories<ChiTietSanPham> chitietsps;
        private readonly IHoaDonServices hoaDonServices;
        AppDbcontext context = new AppDbcontext();
        public HoaDonController()
        {
            hoadons = new AllRepositories<HoaDon>(context, context.HoaDons);
            cthoadons = new AllRepositories<ChiTietHoaDon>(context, context.ChiTietHoaDons);
            chitietsps = new AllRepositories<ChiTietSanPham>(context, context.ChiTietSanPhams);
            hoaDonServices = new HoaDonServices();
           
        }
        // GET: api/<HoaDonController>
        [HttpGet]
        public List<HoaDon> Get()
        {
            return hoadons.GetAll();
        }

        // GET api/<HoaDonController>/5
        [HttpGet("{id}")]
        public HoaDon Get(Guid id)
        {
            return hoadons.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<HoaDonController>
        [HttpPost]
        public bool Post(HoaDonViewModel hoaDonView)
        {
            return hoaDonServices.CreateHoaDon(hoaDonView.ChiTietHoaDons, hoaDonView.NgayThanhToan, hoaDonView.TenNguoiNhan, hoaDonView.SDT, hoaDonView.Email, hoaDonView.DiaChi, hoaDonView.TienKhachDua, hoaDonView.TongTien, hoaDonView.TienShip, hoaDonView.PhuongThucThanhToan, hoaDonView.IdTrangThaiGiaoHang, hoaDonView.IdNguoiDung);
        }
        //public bool Post(HoaDon hoadon)
        //{
           
           
        //    hoadon.Id=Guid.NewGuid();
        //    hoadon.NgayTao = DateTime.Now;

           
            

           
        //    if (hoadon.NgayThanhToan < hoadon.NgayTao)
        //    {
        //        return false;
        //    }
        //    if (hoadon.TienKhachDua < hoadon.TongTien + hoadon.TienShip)
        //    {
        //        return false;
        //    }
        //    hoadon.TienThua = hoadon.TienKhachDua - hoadon.TienShip - hoadon.TongTien;
        //    if (hoadons.Add(hoadon))
        //    {

        //        //List<ChiTietHoaDon> chitiethd = new List<ChiTietHoaDon>();
        //        //foreach(var x in chitiethd)
        //        //{
        //        //    x.Id = Guid.NewGuid();
        //        //    x.IdHoaDon = hoadon.Id;
        //        //    x.DonGia = chitietsps.GetAll().Find(y => y.Id == x.IdChiTiepSP).DonGia;
        //        //    x.TrangThai = 1;
        //        //    cthoadons.Add(x);
        //        //}
                
                


        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        
        //}
        

        // PUT api/<HoaDonController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid IdTrangThaiGiaoHang,int tienShip,DateTime? NgayThanhToan)
        {
            var hoadon = hoadons.GetAll().Where(x => x.Id == id).FirstOrDefault();
            if (hoadon != null)
            {
                hoadon.IdTrangThaiGiaoHang = IdTrangThaiGiaoHang;
                hoadon.TienShip = tienShip;
                hoadon.NgayThanhToan = NgayThanhToan;
                if (hoadon.TienKhachDua < hoadon.TongTien + hoadon.TienShip)
                {
                    return false;
                }
                if (hoadon.NgayThanhToan<hoadon.NgayTao)
                {
                    return false;
                }
                return hoadons.Update(hoadon);
            }
            else
            {
                return false;
            }

        }

        // DELETE api/<HoaDonController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var hoadon = hoadons.GetAll().Where(x => x.Id == id).FirstOrDefault();
            if (hoadon != null)
            {

                return hoadons.Delete(hoadon);
            }
            else
            {
                return false;
            }
        }
    }
}