using App_banAo.Models;
using App_data.Models;
using App_data.ViewModels;
using App_data.ViewModels.ChiTietSPView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Xml.Linq;

namespace App_banAo.Controllers
{
    public class ChiTietSPController : Controller
    {
        private readonly HttpClient httpClients;
        public int PageSize = 9;
        AppDbcontext _dbcontext= new AppDbcontext();

        public ChiTietSPController()
        {
            httpClients = new HttpClient();
        }
        public class PriceRange
        {
            public int Min { get; set; }
            public int Max { get; set; }
        }

        [HttpGet]

        public async Task<IActionResult> GetAllChiTietSanPham(int ProductPage=1)
        {
            //list ctsp
            string apiUrlct = "https://localhost:7016/api/ChiTietSP";
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiDatact);
            ViewBag.ChiTietSanPham = CTSanPhams;
            // list anh 
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //list km
            string apiUrl1 = "https://localhost:7016/api/KhuyenMai";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData1);
            ViewBag.KhuyenMaiView = KhuyenMais;
            //List AllChiTietSP
            string apiUrl = "https://localhost:7016/api/ChiTietSPView";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var AllCTSanPhams = JsonConvert.DeserializeObject<List<AllChiTietSanPham>>(apiData);

            return View(/*CTSanPhams*/

                new ProductListViewModel
                {
                    chiTietSanPhams=AllCTSanPhams
                    .Skip((ProductPage-1)*PageSize).Take(PageSize),
                    PagingInfo=new PagingInfo
                    {ItemsPerPage=PageSize,
                    CurrentPage=ProductPage,
                        TotalItems= CTSanPhams.Count()
                    }
                   
        }
                );
        }
        
        //Tim SanPham Theo Ten
        public async Task<IActionResult> TimSanPhamTheoTen(string TenSanPham, int ProductPage = 1)
        {


            //list ctsp
            string apiUrlct = "https://localhost:7016/api/ChiTietSP";
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiDatact);
            ViewBag.ChiTietSanPham = CTSanPhams;
            // list anh 
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //list km
            string apiUrl1 = "https://localhost:7016/api/KhuyenMai";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData1);
            ViewBag.KhuyenMaiView = KhuyenMais;
            //list All CTSP
            string apiUrl = $"https://localhost:7016/api/ChiTietSPView/TimKiem?name={TenSanPham}";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var AllCTSanPhams = JsonConvert.DeserializeObject<List<AllChiTietSanPham>>(apiData);
            return PartialView(/*CTSanPhams*/"GetAllChiTietSanPham",

              new ProductListViewModel
              {
                  chiTietSanPhams = AllCTSanPhams
                    .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                  PagingInfo = new PagingInfo
                  {
                      ItemsPerPage = PageSize,
                      CurrentPage = ProductPage,
                      TotalItems = CTSanPhams.Count()
                  }

              }
                );
        }
        //Tim kiem San Pham theo price, color, size
        [HttpPost]
        public async Task<IActionResult> GetFilteredProducts([FromBody] FilterData filter, int ProductPage = 1)
        {
            //list ctsp
            string apiUrlct = "https://localhost:7016/api/ChiTietSP";
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiDatact);
            ViewBag.ChiTietSanPham = CTSanPhams;
            
            //list km
            string apiUrl1 = "https://localhost:7016/api/KhuyenMai";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData1);
            ViewBag.KhuyenMaiView = KhuyenMais;
            //List AllChiTietSP
            string apiUrl = "https://localhost:7016/api/ChiTietSPView";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var AllCTSanPhams = JsonConvert.DeserializeObject<List<AllChiTietSanPham>>(apiData);

            var filterdProducts = AllCTSanPhams.ToList();
            // price
            if (filter.PriceRanges != null && filter.PriceRanges.Count > 0 && !filter.PriceRanges.Contains("all"))
            {
                List<PriceRange> priceRanges = new List<PriceRange>();
                foreach (var range in filter.PriceRanges)
                {
                    var value = range.Split("-").ToArray();
                    PriceRange priceRange = new PriceRange();
                    priceRange.Min = Int32.Parse(value[0]);
                    priceRange.Max = Int32.Parse(value[1]);
                    priceRanges.Add(priceRange);
                }
                filterdProducts = filterdProducts.Where(p => priceRanges.Any(r => p.GiaGoc >= r.Min && p.GiaGoc <= r.Max)).ToList();
            }
            // color
             if (filter.Colors != null && filter.Colors.Count > 0 && !filter.Colors.Contains("all"))
            {
                filterdProducts = filterdProducts.Where(p => filter.Colors.Contains(p.TenMauSac)).ToList();
            }
            //size
             if (filter.Sizes != null && filter.Sizes.Count > 0 && !filter.Sizes.Contains("all"))
            {
                filterdProducts = filterdProducts.Where(p => filter.Sizes.Contains(p.TenSize)).ToList();
            }
            return PartialView("SanPhamSearch", filterdProducts);
        }
        public async Task<IActionResult> Details(Guid id)
        {  //list ctsp
            string apiUrlct = "https://localhost:7016/api/ChiTietSP";
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiDatact);
            ViewBag.ChiTietSanPham = CTSanPhams;
            // list anh 
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            // list mau sac
            string apiUrlms = "https://localhost:7016/api/MauSac";
            var responsems = await httpClients.GetAsync(apiUrlms);
            string apiDatams = await responsems.Content.ReadAsStringAsync();
            var MauSacs = JsonConvert.DeserializeObject<List<MauSac>>(apiDatams);
            ViewBag.MauSacs = MauSacs;  
            //list km
            string apiUrl1 = "https://localhost:7016/api/KhuyenMai";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData1);
            ViewBag.KhuyenMaiView = KhuyenMais;
            // list all ct by id    sp 
            string apiUrl = "https://localhost:7016/api/ChiTietSPView/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var AllCTSanPhams = JsonConvert.DeserializeObject<AllChiTietSanPham>(apiData);
            return View(AllCTSanPhams);
        }
        
        [HttpPost]

        public async Task<IActionResult> UpdateMauSac(Guid id,Guid IdMauSac)
        {  

            var url =
                $"https://localhost:7016/api/ChiTietSP/IdMauSac?id={id}&IdMauSac={IdMauSac}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return Json(new { success = true, message = "Cập nhật thành công!" });
            return Json(new { success = false });
        }
        // Update Size CTSP
        
        [HttpPost]

        public async Task<IActionResult> UpdateSize(Guid id,Guid IdSize)
        {
           
            var url =
                $"https://localhost:7016/api/ChiTietSP/IdSize?id={id}&IdSize={IdSize}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return Json(new { success = true, message = "Cập nhật thành công!" });
            return Json(new { success = false });
        }
        // update nhieu thuoc tinh

        #region Admin CTSP
        [HttpGet]

        public async Task<IActionResult> GetAllChiTietSanPhamAdmin(int ProductPage = 1)
        {
            //list ctsp
            string apiUrlct = "https://localhost:7016/api/ChiTietSP";
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiDatact);
            ViewBag.ChiTietSanPham = CTSanPhams;
            // list anh 
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //list km
            string apiUrl1 = "https://localhost:7016/api/KhuyenMai";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData1);
            ViewBag.KhuyenMaiView = KhuyenMais;
            //List AllChiTietSP
            string apiUrl = "https://localhost:7016/api/ChiTietSPView";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var AllCTSanPhams = JsonConvert.DeserializeObject<List<AllChiTietSanPham>>(apiData);

            return View(/*CTSanPhams*/

                new ProductListViewModel
                {
                    chiTietSanPhams = AllCTSanPhams
                    .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = ProductPage,
                        TotalItems = CTSanPhams.Count()
                    }

                }
                );
        }
        [HttpGet]
        public async Task<IActionResult> TimSanPhamTheoTenAdmin(string TenSanPham, int ProductPage = 1)
        {


            //list ctsp
            string apiUrlct = "https://localhost:7016/api/ChiTietSP";
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiDatact);
            ViewBag.ChiTietSanPham = CTSanPhams;
            // list anh 
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //list km
            string apiUrl1 = "https://localhost:7016/api/KhuyenMai";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData1);
            ViewBag.KhuyenMaiView = KhuyenMais;
            //list All CTSP
            string apiUrl = $"https://localhost:7016/api/ChiTietSPView/TimKiem?name={TenSanPham}";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var AllCTSanPhams = JsonConvert.DeserializeObject<List<AllChiTietSanPham>>(apiData);
            return PartialView(/*CTSanPhams*/"GetAllChiTietSanPhamAdmin",

              new ProductListViewModel
              {
                  chiTietSanPhams = AllCTSanPhams
                    .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                  PagingInfo = new PagingInfo
                  {
                      ItemsPerPage = PageSize,
                      CurrentPage = ProductPage,
                      TotalItems = CTSanPhams.Count()
                  }

              }
                );
        }
        public IActionResult Create()
        {
            //// list size
            //string apiUrlsize = "https://localhost:7016/api/Size";
            //var responsesize = await httpClients.GetAsync(apiUrlsize);
            //string apiDatasize = await responsesize.Content.ReadAsStringAsync();
            //var Sizes = JsonConvert.DeserializeObject<List<Size>>(apiDatasize);
            //ViewBag.Size = Sizes;
            //// list loai SP
            //string apiUrllsp = "https://localhost:7016/api/LoaiSP";
            //var responselsp = await httpClients.GetAsync(apiUrllsp);
            //string apiDatalsp = await responselsp.Content.ReadAsStringAsync();
            //var LoaiSPs = JsonConvert.DeserializeObject<List<LoaiSP>>(apiDatalsp);
            //ViewBag.LoaiSPs = LoaiSPs;
            //// list MauSac
            //string apiUrlms = "https://localhost:7016/api/MauSac";
            //var responsems = await httpClients.GetAsync(apiUrlms);
            //string apiDatams = await responsems.Content.ReadAsStringAsync();
            //var MauSacs = JsonConvert.DeserializeObject<List<MauSac>>(apiDatams);
            //ViewBag.MauSacs = MauSacs;
            //// list SanPham
            //string apiUrlsp = "https://localhost:7016/api/SanPham";
            //var responsesp = await httpClients.GetAsync(apiUrlsp);
            //string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            //var SanPhams = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            //ViewBag.SanPhams = SanPhams;
            //// list NCC
            //string apiUrlNCC = "https://localhost:7016/api/NhaCungCap";
            //var responseNCC = await httpClients.GetAsync(apiUrlNCC);
            //string apiDataNCC = await responseNCC.Content.ReadAsStringAsync();
            //var NhaCungCaps = JsonConvert.DeserializeObject<List<NhaCungCap>>(apiDataNCC);
            //ViewBag.NhaCungCap = NhaCungCaps;
            //// list Anh
            //string apiUrlanh = "https://localhost:7016/api/Anh";
            //var responseanh = await httpClients.GetAsync(apiUrlanh);
            //string apiDataanh = await responseanh.Content.ReadAsStringAsync();
            //var Anhs = JsonConvert.DeserializeObject<List<Anh>>(apiDataanh);
            //ViewBag.Anhs = Anhs;
            //// list KMV
            //string apiUrl1 = "https://localhost:7016/api/KhuyenMai";
            //var response1 = await httpClients.GetAsync(apiUrl1);
            //string apiData1 = await response1.Content.ReadAsStringAsync();
            //var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData1);
            //ViewBag.KhuyenMaiView = KhuyenMais;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ChiTietSanPham ctsp)
        {
           
            var response = await httpClients.PostAsync($"https://localhost:7016/api/ChiTietSP?IdKhuyenMai={ctsp.IdKhuyenMai}&DonGia={ctsp.DonGia}&Soluong={ctsp.soluong}&TrangThai={ctsp.TrangThai}&IdAnh={ctsp.IdAnh}&IdMauSac={ctsp.IdMauSac}&IdSanPham={ctsp.IdSanPham}&IdLoaiSp={ctsp.IdLoaiSP}&IdNhaCungCap={ctsp.IdNhaCungCap}&IdSize={ctsp.IdSize}", null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllChiTietSanPhamAdmin");
            return View();
           
            
        }
        public async Task<IActionResult> UpdateCTSP(Guid id)
        {
            // list size
            string apiUrlsize = "https://localhost:7016/api/Size";
            var responsesize = await httpClients.GetAsync(apiUrlsize);
            string apiDatasize = await responsesize.Content.ReadAsStringAsync();
            var Sizes = JsonConvert.DeserializeObject<List<Size>>(apiDatasize);
            ViewBag.Size = Sizes;
            // list loai SP
            string apiUrllsp = "https://localhost:7016/api/LoaiSP";
            var responselsp = await httpClients.GetAsync(apiUrllsp);
            string apiDatalsp = await responselsp.Content.ReadAsStringAsync();
            var LoaiSPs = JsonConvert.DeserializeObject<List<LoaiSP>>(apiDatalsp);
            ViewBag.LoaiSPs = LoaiSPs;
            // list MauSac
            string apiUrlms = "https://localhost:7016/api/MauSac";
            var responsems = await httpClients.GetAsync(apiUrlms);
            string apiDatams = await responsems.Content.ReadAsStringAsync();
            var MauSacs = JsonConvert.DeserializeObject<List<MauSac>>(apiDatams);
            ViewBag.MauSacs = MauSacs;
            // list SanPham
            string apiUrlsp = "https://localhost:7016/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPhams = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPhams = SanPhams;
            // list NCC
            string apiUrlNCC = "https://localhost:7016/api/NhaCungCap";
            var responseNCC = await httpClients.GetAsync(apiUrlNCC);
            string apiDataNCC = await responseNCC.Content.ReadAsStringAsync();
            var NhaCungCaps = JsonConvert.DeserializeObject<List<NhaCungCap>>(apiDataNCC);
            ViewBag.NhaCungCap = NhaCungCaps;
            // list Anh
            string apiUrlanh = "https://localhost:7016/api/Anh";
            var responseanh = await httpClients.GetAsync(apiUrlanh);
            string apiDataanh = await responseanh.Content.ReadAsStringAsync();
            var Anhs = JsonConvert.DeserializeObject<List<Anh>>(apiDataanh);
            ViewBag.Anhs = Anhs;
            // list KMV
            string apiUrl1 = "https://localhost:7016/api/KhuyenMai";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData1);
            ViewBag.KhuyenMaiView = KhuyenMais;
          
           // detail  1 ctsp
            var url = $"https://localhost:7016/api/ChiTietSP/(IdCTSP)?id={id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var CTSP= JsonConvert.DeserializeObject<ChiTietSanPham>(result);
            return View(CTSP);
           
        }
        [HttpPost]
        public async Task<IActionResult> Update(ChiTietSanPham ctsp)
        {

            var response = await httpClients.PutAsync($"https://localhost:7016/api/ChiTietSP/Id?id={ctsp.Id}&IdKhuyenMai={ctsp.IdKhuyenMai}&DonGia={ctsp.DonGia}&Soluong={ctsp.soluong}&TrangThai={ctsp.TrangThai}&IdAnh={ctsp.IdAnh}&IdMauSac={ctsp.IdMauSac}&IdSanPham={ctsp.IdSanPham}&IdLoaiSp={ctsp.IdLoaiSP}&IdNhaCungCap={ctsp.IdNhaCungCap}&IdSize={ctsp.IdSize}", null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllChiTietSanPhamAdmin");
            return View();
            

        }
        
        public async Task<IActionResult> Deletes(Guid id)
        {
            var response = await httpClients.DeleteAsync($"https://localhost:7016/api/ChiTietSP/{id}");
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllChiTietSanPhamAdmin");
            return View();
        }


        #endregion

    }
}
