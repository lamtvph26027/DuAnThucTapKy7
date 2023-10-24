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
        // Update MauSac CTSP
        public IActionResult UpdateMauSac(Guid id)
        {
            var url = $"https://localhost:7016/api/SanPham/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var SanPhams = JsonConvert.DeserializeObject<ChiTietSanPham>(result);
            return View(SanPhams);
        }
        [HttpPost]

        public async Task<IActionResult> UpdateMauSac(ChiTietSanPham sanpham)
        {  

            var url =
                $"https://localhost:7016/api/ChiTietSP/IdMauSac?id={sanpham.Id}&IdMauSac={sanpham.IdMauSac}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllChiTietSanPham");
            return View();
        }
        // Update Size CTSP
        public IActionResult UpdateSize(Guid id)
        {
            var url = $"https://localhost:7016/api/SanPham/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var SanPhams = JsonConvert.DeserializeObject<ChiTietSanPham>(result);
            return View(SanPhams);
        }
        [HttpPost]

        public async Task<IActionResult> UpdateSize(ChiTietSanPham sanpham)
        {
           
            var url =
                $"https://localhost:7016/api/ChiTietSP/IdSize?id={sanpham.Id}&IdSize={sanpham.IdSize}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("Details");
            return View();
        }
    }
}
