using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using App_data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietHoaDonController : ControllerBase
    {
        private readonly IAllRepositories<ChiTietHoaDon> chitiethoadons;
        private readonly IAllRepositories<ChiTietSanPham> chitietsps;
        
        
     
        AppDbcontext context= new AppDbcontext();
        public ChiTietHoaDonController()
        {
            chitiethoadons= new AllRepositories<ChiTietHoaDon>(context,context.ChiTietHoaDons);
            chitietsps= new AllRepositories<ChiTietSanPham>(context,context.ChiTietSanPhams);
            context= new AppDbcontext();    
     
            

        }
        // GET: api/<ChiTietHoaDonController>
        [HttpGet]
        public List<ChiTietHoaDon> GetAll()
        {
            return chitiethoadons.GetAll();
        }
       

        // GET api/<ChiTietHoaDonController>/5
        [HttpGet("{id}")]
        public List<ChiTietHoaDon> GetAllByIdHoaDon(Guid id)
        {
            return chitiethoadons.GetAll().Where(x => x.IdHoaDon == id).ToList();
        }

        // POST api/<ChiTietHoaDonController>
        [HttpPost]
        public string Post(int soluong, int TrangThai,Guid IdChiTietSP,Guid IdHoaDon)
        {
            ChiTietHoaDon chiTietHoaDon= new ChiTietHoaDon();
            chiTietHoaDon.Id=Guid.NewGuid();
            chiTietHoaDon.TrangThai=TrangThai;
            chiTietHoaDon.DonGia = chitietsps.GetAll().Find(x => x.Id == IdChiTietSP).DonGia;
            chiTietHoaDon.SoLuong = soluong;
            chiTietHoaDon.IdChiTiepSP = IdChiTietSP;
            chiTietHoaDon.IdHoaDon = IdHoaDon;
            if (chitiethoadons.GetAll().Exists(p => p.IdChiTiepSP == IdChiTietSP && p.IdHoaDon == IdHoaDon))
            {
                Guid Id = chitiethoadons.GetAll().Find(p => p.IdChiTiepSP == IdChiTietSP && p.IdHoaDon == IdHoaDon).Id;
                ChiTietHoaDon chitiethoadon1 = chitiethoadons.GetAll().Find(p => p.IdChiTiepSP == IdChiTietSP && p.IdHoaDon == IdHoaDon);
                if (chiTietHoaDon.SoLuong + soluong > chitietsps.GetAll().Find(p => p.Id == IdChiTietSP).soluong)
                {
                    return "so luong trong kho khong du";
                }
                else
                {
                    chitiethoadon1.SoLuong = chitiethoadon1.SoLuong + soluong;
                
                    chitiethoadons.Update(chitiethoadon1).ToString();
                    ChiTietSanPham chiTietSanPham = chitietsps.GetAll().Find(x=>x.Id==IdChiTietSP);

                    if (chiTietSanPham != null)
                    {
                        chiTietSanPham.soluong = chiTietSanPham.soluong - soluong;
                        chitietsps.Update(chiTietSanPham).ToString();
                   
                       
                    }
                    return "Thanh toan thanh cong";


                }
            }
            else
            {
               
                chitiethoadons.Add(chiTietHoaDon).ToString();
                ChiTietSanPham chiTietSanPham = chitietsps.GetAll().Find(x => x.Id == IdChiTietSP);

                if (chiTietSanPham != null)
                {
                    chiTietSanPham.soluong = chiTietSanPham.soluong - soluong;
                    chitietsps.Update(chiTietSanPham).ToString();


                }
                return "Thanh toan thanh cong";

            }





        }

        // PUT api/<ChiTietHoaDonController>/5
        [HttpPut("{id}")]
        public string Put(Guid id, int soluong, int TrangThai, Guid IdChiTietSP, Guid IdHoaDon)
        {
            var chiTietHoaDon = chitiethoadons.GetAll().FirstOrDefault(x => x.Id == id);
            if (chiTietHoaDon != null)
            {
                chiTietHoaDon.TrangThai = TrangThai;
                chiTietHoaDon.SoLuong = soluong;
                chiTietHoaDon.IdChiTiepSP = IdChiTietSP;
                chiTietHoaDon.IdHoaDon = IdHoaDon;
                if (chitiethoadons.GetAll().Exists(p => p.IdChiTiepSP == IdChiTietSP && p.IdHoaDon == IdHoaDon))
                {
                    Guid Id = chitiethoadons.GetAll().Find(p => p.IdChiTiepSP == IdChiTietSP && p.IdHoaDon == IdHoaDon).Id;
                    ChiTietHoaDon chitiethoadon1 = chitiethoadons.GetAll().Find(p => p.IdChiTiepSP == IdChiTietSP && p.IdHoaDon == IdHoaDon);
                    if (chiTietHoaDon.SoLuong + soluong > chitietsps.GetAll().Find(p => p.Id == IdChiTietSP).soluong)
                    {
                        return "so luong trong kho khong du";
                    }
                    else
                    {
                        chitiethoadon1.SoLuong =  soluong;
                    
                        return chitiethoadons.Update(chitiethoadon1).ToString();
                        var tru = chitietsps.GetAll().Find(x => x.Id == IdChiTietSP).soluong = chitietsps.GetAll().Find(x => x.Id == IdChiTietSP).soluong - soluong;
                        return tru.ToString();
                    }
                }
                else
                {
                    return chitiethoadons.Update(chiTietHoaDon).ToString();
                    
                }
            }
            else
            {
                return "loi";
            }
        }

        // DELETE api/<ChiTietHoaDonController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var chiTietHoaDon = chitiethoadons.GetAll().FirstOrDefault(x => x.Id == id);
            if (chiTietHoaDon != null)
            {
                return chitiethoadons.Delete(chiTietHoaDon);
            }
            else
            {
                return false;
            }
        }
    }
}
