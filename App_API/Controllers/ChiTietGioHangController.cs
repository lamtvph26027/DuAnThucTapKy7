using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietGioHangController : ControllerBase
    {
        private readonly IAllRepositories<ChiTietGioHang> repos;
        private readonly IAllRepositories<ChiTietSanPham> chitietsp;
        

        AppDbcontext context = new AppDbcontext();
        public ChiTietGioHangController()
        {
            repos = new AllRepositories<ChiTietGioHang>(context,context.ChiTietGioHangs);
            chitietsp = new AllRepositories<ChiTietSanPham>(context,context.ChiTietSanPhams);
         

        }
        // GET: api/<ChiTietGioHangController>
        [HttpGet]
        public List<ChiTietGioHang> Get()
        {
            return repos.GetAll();
        }

        // GET api/<ChiTietGioHangController>/5
        [HttpGet("{id}")]
        public ChiTietGioHang Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x=>x.Id==id);
        }

        // POST api/<ChiTietGioHangController>
        [HttpPost]
        public string Post(int soluong, Guid IdChiTietSp, Guid IdNguoiDung)
        {
            ChiTietGioHang chiTietGioHang = new ChiTietGioHang();
            chiTietGioHang.Id = Guid.NewGuid();
            chiTietGioHang.DonGia = chitietsp.GetAll().Find(x => x.Id == IdChiTietSp).DonGia;
            chiTietGioHang.SoLuong = soluong;
            chiTietGioHang.IdChiTietSP = IdChiTietSp;
            chiTietGioHang.IdNguoiDung = IdNguoiDung;
            
            
            if (repos.GetAll().Exists(p => p.IdChiTietSP == IdChiTietSp && p.IdNguoiDung == IdNguoiDung))
            {
                Guid Id = repos.GetAll().Find(p => p.IdChiTietSP == IdChiTietSp && p.IdNguoiDung == IdNguoiDung).Id;
                ChiTietGioHang chitietgiohang1 = repos.GetAll().Find(p => p.IdChiTietSP == IdChiTietSp && p.IdNguoiDung == IdNguoiDung);
                if (chiTietGioHang.SoLuong + soluong > chitietsp.GetAll().Find(p => p.Id == IdChiTietSp).soluong)
                {
                    return "so luong trong kho khong du";
                }
                else
                {
                    chitietgiohang1.SoLuong = chitietgiohang1.SoLuong + soluong;
                    return repos.Update(chitietgiohang1).ToString();
                }
            }
            else
            {
                return repos.Add(chiTietGioHang).ToString();
            }
        }

        // PUT api/<ChiTietGioHangController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, int soluong, Guid IdChiTietSp, Guid IdNguoiDung)
        {
            var chiTietGioHang = repos.GetAll().FirstOrDefault(x => x.Id == id);
            if (chiTietGioHang != null)
            {
                chiTietGioHang.SoLuong = soluong;
                chiTietGioHang.IdChiTietSP = IdChiTietSp;
                chiTietGioHang.DonGia = chitietsp.GetAll().Find(x => x.Id == IdChiTietSp).DonGia;
                chiTietGioHang.IdNguoiDung = IdNguoiDung;
                return  repos.Update(chiTietGioHang);

        }
            else
            {
                return false;
            }
        }

    //// DELETE api/<ChiTietGioHangController>/5
    [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var chiTietGioHang = repos.GetAll().FirstOrDefault(x => x.Id == id);
            if (chiTietGioHang != null)
            {

                return repos.Delete(chiTietGioHang);
            }
            else
            {
                return false;
            }
        }
    }
}
