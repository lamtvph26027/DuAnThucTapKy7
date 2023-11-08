using App_banAo.Models;
using App_banAo.Services;
using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using App_data.ViewModels;
using App_data.ViewModels.ChiTietSPView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http;

namespace App_banAo.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IAllRepositories<HoaDon> hoadons;
        private readonly IAllRepositories<ChiTietHoaDon> chitiethoadonss;
        private readonly AppDbcontext context;
        public int PageSize = 9;
        List<ChiTietGioHang> listCTGH = new List<ChiTietGioHang>();
        List<HoaDon> ListHD = new List<HoaDon>();
        List<ChiTietSanPham> ListCTSP = new List<ChiTietSanPham>();
        List<ChiTietHoaDon> listCTHD = new List<ChiTietHoaDon>();
        public ThanhToanController()
        {
            _httpClient = new HttpClient();
            context = new AppDbcontext();
            //hoadons = new AllRepositories<HoaDon>(context, context.HoaDons);
            chitiethoadonss = new AllRepositories<ChiTietHoaDon>(context, context.ChiTietHoaDons);
            HttpResponseMessage response1 = _httpClient.GetAsync("https://localhost:7016/api/ChiTietGioHang").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                listCTGH = JsonConvert.DeserializeObject<List<ChiTietGioHang>>(data);
            }
            HttpResponseMessage response2 = _httpClient.GetAsync("https://localhost:7016/api/HoaDon").Result;
            if (response2.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;
                ListHD = JsonConvert.DeserializeObject<List<HoaDon>>(data);
                
            }
            HttpResponseMessage response3 = _httpClient.GetAsync("https://localhost:7016/api/ChiTietHoaDon").Result;
            if (response3.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;
                listCTHD = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(data);

            }
            HttpResponseMessage response4 = _httpClient.GetAsync("https://localhost:7016/api/ChiTietSP").Result;
            if (response4.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                ListCTSP = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(data);
            }
        }
        public async Task<IActionResult> GetAllHoaDon(int ProductPage = 1)
        {
            string apiUrl = "https://localhost:7016/api/HoaDon";
            var response = await _httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sanPhams = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);

            return View(new PhanTrangThanhToanAdmin
            {
                listhoadons = sanPhams
         .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = sanPhams.Count()
                }
            });
        }
        public async Task<IActionResult> Details(Guid id)
        {
            //list km
            string apiUrl1 = "https://localhost:7016/api/KhuyenMai";
            var response1 = await _httpClient.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData1);
            ViewBag.KhuyenMaiView = KhuyenMais;
            //list chi tiet hoa don
            string apiUrl = "https://localhost:7016/api/ChiTietHoaDon/"+id;
            var response = await _httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sanPhams = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(apiData);
            return View(sanPhams);
        }
     
        public async Task<IActionResult> Deletes(Guid id)
        {
            var url = $"https://localhost:7016/api/HoaDon/{id}";
            var response = await _httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllHoaDon");
            return BadRequest();
        }
        public async Task<IActionResult> TaoHoaDon(long tongtien)
        {
            ViewData["TongTien"] = tongtien;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TaoHoaDon(HoaDonViewModel hoaDonViewModel)
        {
            string apiUrl = "https://localhost:7016/api/ChiTietSP";
            var response = await _httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var ctsanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiData);
           
            hoaDonViewModel.PhuongThucThanhToan = "TTKNH";
            hoaDonViewModel.IdTrangThaiGiaoHang = Guid.Parse("febe0fa7-ee12-40cb-a0de-483b951ff414");
            hoaDonViewModel.TienShip = 0;
            //hoaDonViewModel.DiaChi = "123455432344";
            hoaDonViewModel.ChiTietHoaDons = new List<ChiTietHoaDonView>();
            if (HttpContext.Session.GetString("IdUser") == null)
            {
                foreach (var x in GioHangServices.GetObjFormSession(HttpContext.Session, "Cart"))
                {
                    ChiTietHoaDonView cthd = new ChiTietHoaDonView();

                    cthd.IdChiTiepSP = x.IdChiTietSP;
                    cthd.SoLuong = x.SoLuong;
                    hoaDonViewModel.ChiTietHoaDons.Add(cthd);
                }
                foreach (var x in GioHangServices.GetObjFormSession(HttpContext.Session, "Cart"))
                {

                    var tim = ctsanPhams.FirstOrDefault(y => y.Id == x.IdChiTietSP);
                   if(tim!= null)
                    {
                        tim.soluong =tim.soluong- x.SoLuong;
                        context.ChiTietSanPhams.Update(tim);
                        context.SaveChangesAsync();
                    }
                   




                }


                var CartUser = GioHangServices.GetObjFormSession(HttpContext.Session, "Cart");
                // Xóa tất cả sản phẩm trong giỏ hàng
                CartUser.Clear();
                GioHangServices.SetObjToJson(HttpContext.Session, "Cart", CartUser);// Cập nhật lại giỏ hàng
                var response2 = await _httpClient.PostAsJsonAsync("https://localhost:7016/api/HoaDon", hoaDonViewModel);

                if (response2.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetAllHoaDon", "ThanhToan");
                }
                else return RedirectToAction("Contact", "Home");


            }
            else
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                hoaDonViewModel.IdNguoiDung = iduser;
                foreach (var x in listCTGH.Where(c => c.IdNguoiDung == iduser).ToList())
                {
                    ChiTietHoaDonView cthd = new ChiTietHoaDonView();

                    cthd.IdChiTiepSP = x.IdChiTietSP;
                    cthd.SoLuong = x.SoLuong;
                    hoaDonViewModel.ChiTietHoaDons.Add(cthd);

                }
                foreach (var x in GioHangServices.GetObjFormSession(HttpContext.Session, "Cart"))
                {

                    var tim = ctsanPhams.FirstOrDefault(y => y.Id == x.IdChiTietSP);
                    if (tim != null)
                    {
                        tim.soluong = tim.soluong - x.SoLuong;
                        context.ChiTietSanPhams.Update(tim);
                        context.SaveChangesAsync();
                    }





                }
                string apiUrlGioHang = "https://localhost:7016/api/ChiTietGioHang";
                var responseGioHang = await _httpClient.GetAsync(apiUrlGioHang);
                string apiDataGioHang = await responseGioHang.Content.ReadAsStringAsync();
                var ChitietGH = JsonConvert.DeserializeObject<List<ChiTietGioHang>>(apiDataGioHang);
                var getallChiTietGioHangbyName = ChitietGH.Where(p => p.IdNguoiDung == iduser).ToList();
                foreach (var item in getallChiTietGioHangbyName)
                {
                    var url = $"https://localhost:7016/api/ChiTietGioHang/{item.Id}";
                    var res = await _httpClient.DeleteAsync(url);
                }
                var response1 = await _httpClient.PostAsJsonAsync("https://localhost:7016/api/HoaDon", hoaDonViewModel);

                if (response1.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetAllHoaDon", "ThanhToan");
                }
                else return RedirectToAction("Contact", "Home");
            }
                
        }
           

        
        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7016/api/HoaDon/{id}";
            var response = _httpClient.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var SanPhams = JsonConvert.DeserializeObject<HoaDon>(result);
            return View(SanPhams);
        }
        [HttpPost]

        public async Task<IActionResult> Update(HoaDon hoadon)
        {
            DateTime? ngaytao = hoadon.NgayThanhToan;
            //var url =
            //    $"https://localhost:7016/api/HoaDon/{hoadon.Id}?IdTrangThaiGiaoHang={hoadon.IdTrangThaiGiaoHang}&tienShip={hoadon.TienShip}&NgayThanhToan={ngaytao?.ToString("MM-dd-yyyy")}";
            var url = $"https://localhost:7016/api/HoaDon/{hoadon.Id}?IdTrangThaiGiaoHang={hoadon.IdTrangThaiGiaoHang}&tienShip={hoadon.TienShip}&NgayThanhToan={ngaytao?.ToString("MM-dd-yyyy")}";
            var response = await _httpClient.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllHoaDon");
            return View();
        }
    }
}
