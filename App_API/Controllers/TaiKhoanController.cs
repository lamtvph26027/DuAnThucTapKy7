using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly IAllRepositories<TaiKhoan> taikhoans;
        AppDbcontext context = new AppDbcontext();
        public TaiKhoanController()
        {
            taikhoans = new AllRepositories<TaiKhoan>(context, context.TaiKhoans);
        }
        // GET: api/<TaiKhoanController>
        [HttpGet]
        public List<TaiKhoan> Get()
        {
            return taikhoans.GetAll();
        }
        [Route("idkt")]
        [HttpGet]
        public List<TaiKhoan> GetTKKH( Guid id )
        {
            return taikhoans.GetAll().Where(x=>x.IdVaiTro!=id).ToList();
        }

        // GET api/<TaiKhoanController>/5
        [HttpGet("{id}")]
        public TaiKhoan Get(Guid id)
        {
            return taikhoans.GetAll().FirstOrDefault(x => x.IdNguoiDung == id);
        }
      

        // POST api/<TaiKhoanController>
        [HttpPost]
        public bool Post(string ten, string tendem, string ho, DateTime ngaysinh, string password, int gioitinh, string email, string diachi, string sdt,int TrangThai, Guid idvaitro)
        {
            TaiKhoan taikhoan = new TaiKhoan();
            taikhoan.IdNguoiDung = Guid.NewGuid();
            taikhoan.Ho = ho;
            taikhoan.Ten = ten;
            taikhoan.TenDem = tendem;
            taikhoan.GioiTinh = gioitinh;
            taikhoan.NgaySinh = ngaysinh;
            taikhoan.Password = password;
            taikhoan.Email = email;
            taikhoan.DiaChi = diachi;
            taikhoan.SDT = sdt;
            taikhoan.TrangThai = TrangThai;
            taikhoan.IdVaiTro = idvaitro;
            return taikhoans.Add(taikhoan);
        }

        // PUT api/<TaiKhoanController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string ten, string tendem, string ho, DateTime ngaysinh, string password, int gioitinh, string email, string diachi, string sdt, int TrangThai)
        {
            var taikhoan= taikhoans.GetAll().FirstOrDefault(x => x.IdNguoiDung == id);
            if (taikhoan != null)
            {
                taikhoan.Ho = ho;
                taikhoan.Ten = ten;
                taikhoan.TenDem = tendem;
                taikhoan.GioiTinh = gioitinh;
                taikhoan.NgaySinh = ngaysinh;
                taikhoan.Password = password;
                taikhoan.Email = email;
                taikhoan.DiaChi = diachi;
                taikhoan.SDT = sdt;
                taikhoan.TrangThai = TrangThai;
                return taikhoans.Update(taikhoan);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<TaiKhoanController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var taikhoan = taikhoans.GetAll().FirstOrDefault(x => x.IdNguoiDung == id);
            if (taikhoan != null)
            {
                
                return taikhoans.Delete(taikhoan);
            }
            else
            {
                return false;
            }
        }
    }
}
