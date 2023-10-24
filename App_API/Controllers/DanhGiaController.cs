using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhGiaController : ControllerBase
    {
        private readonly IAllRepositories<DanhGia> danhgias;
        AppDbcontext context= new AppDbcontext();
        public DanhGiaController()
        {
            danhgias= new AllRepositories<DanhGia>(context,context.DanhGias);
        }
        // GET: api/<DanhGiaController>
        [HttpGet]
        public List<DanhGia> Get()
        {
            return danhgias.GetAll();
        }

        // GET api/<DanhGiaController>/5
        [HttpGet("{id}")]
        public DanhGia Get(Guid id)
        {
            return danhgias.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<DanhGiaController>
        [HttpPost]
        public bool Post(string BinhLuan,Guid IdNguoidung,Guid IdChiTietSP,int Sao,int TrangThai)
        {
            DanhGia danhGia = new DanhGia();
            danhGia.Id=Guid.NewGuid();
            danhGia.BinhLuan=BinhLuan;
            danhGia.IdNguoiDung = IdNguoidung;
            danhGia.IdChiTietSP=IdChiTietSP;
            danhGia.Sao=Sao;
            danhGia.TrangThai=TrangThai;
            return danhgias.Add(danhGia);
        }

        // PUT api/<DanhGiaController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string BinhLuan, Guid IdNguoidung, Guid IdChiTietSP, int Sao, int TrangThai)
        {
            var danhGia= danhgias.GetAll().FirstOrDefault(x => x.Id == id);
            if (danhGia != null)
            {
                danhGia.BinhLuan = BinhLuan;
                danhGia.IdNguoiDung = IdNguoidung;
                danhGia.IdChiTietSP = IdChiTietSP;
                danhGia.Sao = Sao;
                danhGia.TrangThai = TrangThai;
                return danhgias.Update(danhGia);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<DanhGiaController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var danhGia = danhgias.GetAll().FirstOrDefault(x => x.Id == id);
            if (danhGia != null)
            {
              
                return danhgias.Delete(danhGia);
            }
            else
            {
                return false;
            }
        }
    }
}
