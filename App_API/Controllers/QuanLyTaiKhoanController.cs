using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyTaiKhoanController : ControllerBase
    {
        private readonly IAllRepositories<TaiKhoan> _respos;
       AppDbcontext context= new AppDbcontext();
      
        public QuanLyTaiKhoanController()
        {
         
            _respos=new AllRepositories<TaiKhoan>(context,context.TaiKhoans);
           
        }

        
       
       

        // POST api/<QuanLyTaiKhoanController>
        [HttpPost("DangKyTaiKhoan")]
        public string DangKyTaiKhoan(string ten, DateTime ngaysinh, int gioitinh, string email, int trangThai, string diachi, string sdt,Guid IdVaiTro, string password, string resetPassword)
        {
            try
            {
                TaiKhoan taikhoan = new TaiKhoan();
                taikhoan.IdNguoiDung = Guid.NewGuid();

                taikhoan.Ten = ten;

                taikhoan.GioiTinh = gioitinh;
                taikhoan.NgaySinh = ngaysinh;

                taikhoan.Email = email;
                taikhoan.DiaChi = diachi;
                var CheckTrungSDT = _respos.GetAll().FirstOrDefault(x => x.SDT == sdt);
                if (CheckTrungSDT != null)
                {
                    return "trung so dien thoai";

                }
                taikhoan.SDT = sdt;


                taikhoan.TrangThai = 1;

                taikhoan.IdVaiTro = IdVaiTro;
                taikhoan.Password = password;
                if (resetPassword != password)
                {
                    return "mat khau nhap khong khop";
                }
                return  _respos.Add(taikhoan).ToString();
               
            }
            catch
            {
                return "dang ki khong thanh cong";
            }
          
        }

        // PUT api/<QuanLyTaiKhoanController>/5
        [HttpPut("DoiMatKhau")]
        public string Put(string email, string oldPassword, string newPassword,string resetNewPassword)
        {
           var taiKhoan=_respos.GetAll().FirstOrDefault(x=>x.Email== email&&x.Password==oldPassword);
            if(taiKhoan!=null)
            {
                taiKhoan.Password = newPassword;
                if(resetNewPassword != newPassword)
                {
                    return "mat khau moi chua khop";   
                }
             return   _respos.Update(taiKhoan).ToString();
                
            }
            else
            {
                return "doi mat khau khong thanh cong";
            }
        }
        // PUT api/<QuanLyTaiKhoanController>/5
        [HttpGet("QuenMatKhau")]
        public string LayLaiMatKhau(string email)
        {
            var tim= _respos.GetAll().FirstOrDefault(x=>x.Email== email);
            if (tim != null)
            {
                return tim.Password;
            }
            else
            {
                return null;
            }
        }


    }
}
