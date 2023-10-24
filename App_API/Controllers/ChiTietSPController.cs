using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietSPController : ControllerBase
    {
        private readonly IAllRepositories<ChiTietSanPham> chitietsps;
        private readonly IAllRepositories<KhuyenMai> _khuyenmai;
        AppDbcontext context = new AppDbcontext();
        public ChiTietSPController()
        {
            chitietsps = new AllRepositories<ChiTietSanPham>(context, context.ChiTietSanPhams);
            _khuyenmai = new AllRepositories<KhuyenMai>(context, context.KhuyenMais);
        }
        // GET: api/<ChiTietSPController>
        [HttpGet]
        public List<ChiTietSanPham> Get()
        {
            return chitietsps.GetAll();
        }

        // GET api/<ChiTietSPController>/5
        [HttpGet("{id}")]
        public ChiTietSanPham Get(Guid id)
        {
            return chitietsps.GetAll().FirstOrDefault(x => x.IdSanPham == id);
        }
        [Route("TimMauSac")]
        [HttpGet]
        public ChiTietSanPham Getmausac(Guid id,Guid Idmausac)
        {
            return chitietsps.GetAll().FirstOrDefault(x => x.IdSanPham == id&&x.IdMauSac==Idmausac);
        }

        //[Route("IdSanPham")]
        //[HttpGet]
        //public List<ChiTietSanPham> GetByIdSanPham(Guid idSanPham)
        //{
        //    return  chitietsps.GetAll().Where(x => x.IdSanPham == idSanPham).ToList();
        //}

        // POST api/<ChiTietSPController>
        [HttpPost]
        public bool Post(Guid? IdKhuyenMai,int DonGia,int Soluong,int TrangThai, Guid IdAnh, Guid IdMauSac,Guid IdSanPham,Guid IdLoaiSp,Guid IdNhaCungCap,Guid IdSize)
        {
            ChiTietSanPham chitietsp = new ChiTietSanPham();
            chitietsp.Id=Guid.NewGuid();
            chitietsp.IdKhuyenMai = IdKhuyenMai;
            chitietsp.TrangThai = TrangThai;
          chitietsp.DonGia = DonGia;    
           
            chitietsp.soluong=Soluong;
            chitietsp.IdAnh=IdAnh;
            chitietsp.IdMauSac=IdMauSac;
            chitietsp.IdSanPham = IdSanPham;
            chitietsp.IdLoaiSP=IdLoaiSp;
            chitietsp.IdNhaCungCap = IdNhaCungCap;
            chitietsp.IdSize = IdSize;
        
            return chitietsps.Add(chitietsp);

        }

        // PUT api/<ChiTietSPController>/5
        [Route("Id")]
        [HttpPut]
        public bool Put(Guid id, Guid? IdKhuyenMai, int DonGia, int Soluong, int TrangThai, Guid IdAnh, Guid IdMauSac, Guid IdSanPham, Guid IdLoaiSp, Guid IdNhaCungCap, Guid IdSize)
        {
            var chitietsp= chitietsps.GetAll().FirstOrDefault(x => x.Id == id);
            if (chitietsp != null)
            {
                chitietsp.IdKhuyenMai = IdKhuyenMai;
                chitietsp.TrangThai = TrangThai;
                chitietsp.DonGia = DonGia;
                chitietsp.soluong = Soluong;
                chitietsp.IdAnh = IdAnh;
                chitietsp.IdMauSac = IdMauSac;
                chitietsp.IdSanPham = IdSanPham;
                chitietsp.IdLoaiSP = IdLoaiSp;
                chitietsp.IdNhaCungCap = IdNhaCungCap;
                chitietsp.IdSize = IdSize;

                return chitietsps.Update(chitietsp);
            }
            else
            {
                return false;
            }
        }
        //[Route("IdMauSac")]
        //[HttpPut]
        //public bool Put(Guid id,  Guid IdMauSac)
        //{
        //    var chitietsp = chitietsps.GetAll().FirstOrDefault(x => x.Id == id);
        //    if (chitietsp != null)
        //    {
        //        chitietsp.IdMauSac = IdMauSac;
               

        //        return chitietsps.Update(chitietsp);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //[Route("IdSize")]
        //[HttpPut]
        //public bool PutSize(Guid id, Guid IdSize)
        //{
        //    var chitietsp = chitietsps.GetAll().FirstOrDefault(x => x.Id == id);
        //    if (chitietsp != null)
        //    {
        //        chitietsp.IdSize= IdSize;



        //        return chitietsps.Update(chitietsp);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        // DELETE api/<ChiTietSPController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var chitietsp = chitietsps.GetAll().FirstOrDefault(x => x.Id == id);
            if (chitietsp != null)
            {
                
                return chitietsps.Delete(chitietsp);
            }
            else
            {
                return false;
            }
        }
    }
}
