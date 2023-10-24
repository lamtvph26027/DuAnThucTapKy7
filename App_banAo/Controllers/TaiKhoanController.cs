using App_banAo.Models;
using App_data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace App_banAo.Controllers
{
    public class TaiKhoanController : Controller
    {
        HttpClient client = new HttpClient();
        AppDbcontext dbcontext = new AppDbcontext();

        public TaiKhoanController()
        {
          

        }
        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginModelView();
            return View(response);
        }
        [HttpPost]
        public IActionResult Login(LoginModelView user)
        {
            if (!ModelState.IsValid) return View(user);
            HttpResponseMessage response = client.GetAsync("https://localhost:7016/api/TaiKhoan").Result;
            List<TaiKhoan> listNgDung = new List<TaiKhoan>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listNgDung = JsonConvert.DeserializeObject<List<TaiKhoan>>(data);
            }
            HttpResponseMessage response1 = client.GetAsync("https://localhost:7016/api/VaiTro").Result;
            List<VaiTro> listVaiTro = new List<VaiTro>();
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                listVaiTro = JsonConvert.DeserializeObject<List<VaiTro>>(data);
            }
            var ngdung = listNgDung.Where(c => c.Email == user.Email).FirstOrDefault();
            if (ngdung != null)
            {
                if (user.Password.Equals(ngdung.Password))
                {
                    HttpContext.Session.SetString("IdUser", ngdung.IdNguoiDung.ToString());
                    HttpContext.Session.SetString("Email", ngdung.Email);
                    var quyen = listVaiTro.Where(c => c.Ten == "Admin").First();
                    if (ngdung.IdVaiTro == quyen.Id)
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (!ModelState.IsValid) return View(user);
                }
            }
            return View(user);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> DangKyTaiKhoan()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> DangKyTaiKhoan(TaiKhoan taikhoan, string resssetpassword)
        {


            if (taikhoan.Password == resssetpassword)
            {



                DateTime ngaysinh = taikhoan.NgaySinh;
                var url = $"https://localhost:7016/api/TaiKhoan?ten={taikhoan.Ten}&tendem={taikhoan.TenDem}&ho={taikhoan.Ho}&ngaysinh={ngaysinh.ToString("MM-dd-yyyy")}&password={taikhoan.Password}&gioitinh={taikhoan.GioiTinh}&email={taikhoan.Email}&diachi={taikhoan.DiaChi}&sdt={taikhoan.SDT}&TrangThai=1&idvaitro=de5dff41-2898-4d9b-8d39-95ae3382f042";
                var response = await client.PostAsync(url, null);
                if (response.IsSuccessStatusCode) return RedirectToAction("Login");
                return View();

            }
            else
            {
                return BadRequest("mat khau nhap lai khong khop");
            }



        }
        [HttpGet]
        public IActionResult ValidateSDT(string sdt)
        {
            List<TaiKhoan> listNgDung = new List<TaiKhoan>();
            try
            {
                var tim = listNgDung.Find(x => x.SDT == sdt);
                if (tim != null)
                    return Json(data: "so dien thoai" + sdt + "da dc dang ky cho 1 tai khoan khac");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        [HttpGet]
        public IActionResult ValidateEmail(string email)
        {
            List<TaiKhoan> listNgDung = new List<TaiKhoan>();
            try
            {
                var tim = listNgDung.Find(x => x.Email == email);
                if (tim != null)
                    return Json(data: "Email" + email + "da dc dang ky cho 1 tai khoan khac");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPut]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            try
            {
                var iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                if (iduser == null)
                {
                    RedirectToAction("Login");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var taikhoan = dbcontext.TaiKhoans.FirstOrDefault(x => x.IdNguoiDung == iduser);
                        if (taikhoan == null)

                            return RedirectToAction("Login");
                        var pass = (change.PasswordOld.Trim() + taikhoan.Password);
                        if (pass == taikhoan.Password)
                        {
                            string passnew = (change.PasswordNew.Trim() + taikhoan.Password);
                            taikhoan.Password = passnew;
                            dbcontext.Update(taikhoan);
                            dbcontext.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }
        public IActionResult UpdateTT()
        {
            Guid idtk = Guid.Parse(HttpContext.Session.GetString("IdUser"));
            ViewData["Success"] = "";
            var url = $"https://localhost:7016/api/TaiKhoan/{idtk}";
            var response = client.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var TaiKhoans = JsonConvert.DeserializeObject<TaiKhoan>(result);
            return View(TaiKhoans);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTT(TaiKhoan taiKhoan, string confirm)
        {
            if (taiKhoan.Password != confirm)
            {
                ViewData["Success"] = "Số điện thoại hoặc mật khẩu nhập lại không chính xác";
            }
            else if (taiKhoan.Password.Length < 6 || taiKhoan.SDT.Length < 10)
            {
                ViewData["Success"] = "Số điện thoại hoặc mật khẩu nhập lại không chính xác";
            }
            else
            {
                ViewData["Success"] = "Thay đổi thông tin thành công";
                var url = $"https://localhost:7016/api/TaiKhoan/{taiKhoan.IdNguoiDung}?ten={taiKhoan.Ten}&TrangThai={taiKhoan.TrangThai}&tendem={taiKhoan.TenDem}&ho={taiKhoan.Ho}&password={taiKhoan.Password}&gioitinh={taiKhoan.GioiTinh}&ngaysinh={taiKhoan.NgaySinh}&email={taiKhoan.Email}&sdt={taiKhoan.SDT}&diachi={taiKhoan.DiaChi}";
                var response = await client.PutAsync(url, null);
                return View();
            }
            return View();
        }
        public IActionResult UpdateTTAdmin()
        {
            Guid idtk = Guid.Parse(HttpContext.Session.GetString("IdUser"));
            ViewData["Success"] = "";
            var url = $"https://localhost:7016/api/TaiKhoan/{idtk}";
            var response = client.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var TaiKhoans = JsonConvert.DeserializeObject<TaiKhoan>(result);
            return View(TaiKhoans);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTTAdmin(TaiKhoan taiKhoan, string confirm)
        {
            if (taiKhoan.Password != confirm)
            {
                ViewData["Success"] = "Số điện thoại hoặc mật khẩu nhập lại không chính xác";
            }
            else if (taiKhoan.Password.Length < 6 || taiKhoan.SDT.Length < 10)
            {
                ViewData["Success"] = "Số điện thoại hoặc mật khẩu nhập lại không chính xác";
            }

            else
            {
                ViewData["Success"] = "Thay đổi thông tin thành công";
                var url = $"https://localhost:7016/api/TaiKhoan/{taiKhoan.IdNguoiDung}?ten={taiKhoan.Ten}&TrangThai={taiKhoan.TrangThai}&tendem={taiKhoan.TenDem}&ho={taiKhoan.Ho}&password={taiKhoan.Password}&gioitinh={taiKhoan.GioiTinh}&ngaysinh={taiKhoan.NgaySinh}&email={taiKhoan.Email}&sdt={taiKhoan.SDT}&diachi={taiKhoan.DiaChi}";
                var response = await client.PutAsync(url, null);
                return View();
            }
            return View();

        }
        public async Task<IActionResult> GetAllTKKH()
        {
            string apiUrl = "https://localhost:7016/api/TaiKhoan/idkt?id=c71183fd-d158-45d6-92f5-b853cac59257";
            var response = await client.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var TaiKhoans = JsonConvert.DeserializeObject<List<TaiKhoan>>(apiData);
            //Guid idad = dbcontext.VaiTros.FirstOrDefault(x => x.Ten == "Admin").Id;
            //TaiKhoans.Where(x => x.IdVaiTro != idad);
            return View(TaiKhoans);
        }


    } 
}
