using App_banAo.Models;
using App_data.Models;
using App_data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PagedList;
using System.Linq;
using System.Net.Http;


namespace App_banAo.Controllers
{
    
   
    public class SanPhamController : Controller
    {
      
        private readonly HttpClient httpClients;
        private readonly AppDbcontext dbcontext;
        public int PageSize = 9;

        public SanPhamController()
        {
            httpClients = new HttpClient();
            dbcontext = new AppDbcontext();
        }
       public class PriceRange
        {
            public int Min { get; set; }
            public int Max { get; set; }    
        }

        [HttpGet ]
     
        public async Task<IActionResult> GetAllSanPham(int ProductPage = 1)
        {
            //List AllChiTietSP
            string apiUrl1 = "https://localhost:7016/api/ChiTietSP";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiData1);
            ViewBag.ChiTietSanPham = CTSanPhams;
            //List Anh
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            // list SP
            string apiUrl = "https://localhost:7016/api/SanPham";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SanPhams = JsonConvert.DeserializeObject<List<SanPham>>(apiData);
           

            return View(new SanPhamViewModel
            {
                SanPhams = SanPhams
                    .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = SanPhams.Count()
                }

            }
                );
        }
        //Tim kiem San Pham Theo Ten
        [HttpGet]
        public async Task<IActionResult> Search(string Ten,int ProductPage = 1)
        {
            string apiUrlct = "https://localhost:7016/api/ChiTietSP";
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiDatact);
            ViewBag.ChiTietSanPham = CTSanPhams;
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            string apiUrl = $"https://localhost:7016/api/SanPham/ten?ten={Ten}";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SanPhams = JsonConvert.DeserializeObject<List<SanPham>>(apiData);


            return PartialView("GetAllSanPham",new SanPhamViewModel
            {
                SanPhams = SanPhams
                    .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = SanPhams.Count()
                }

            }
                );
        }
        //Tim kiem San Pham theo price, color, size
       
      public async Task<IActionResult> GetFilteredProducts([FromBody] FilterData filter)
        {

            //List Anh
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //List MauSac
            string urlMauSac = "https://localhost:7016/api/MauSac";
            var responseMauSac = await httpClients.GetAsync(urlMauSac);
            string DataMauSac = await responseMauSac.Content.ReadAsStringAsync();
            var MauSac = JsonConvert.DeserializeObject<List<MauSac>>(DataMauSac);
            ViewBag.MauSac = MauSac;
            //List Size
            string urlSize = "https://localhost:7016/api/Size";
            var responseSize = await httpClients.GetAsync(urlSize);
            string DataSize = await responseSize.Content.ReadAsStringAsync();
            var Size = JsonConvert.DeserializeObject<List<Size>>(DataSize);
            ViewBag.Size = Size;
            // list ChiTietSP

            string apiUrlct = "https://localhost:7016/api/ChiTietSP";
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiDatact);
            ViewBag.ChiTietSanPham = CTSanPhams;
            var filterdProducts = CTSanPhams.ToList();
            // price
            if(filter.PriceRanges != null&& filter.PriceRanges.Count >0 && !filter.PriceRanges.Contains("all")){
                List<PriceRange> priceRanges = new List<PriceRange>();
                foreach(var range in filter.PriceRanges)
                {
                    var value=range.Split("-").ToArray();
                    PriceRange priceRange = new PriceRange();
                    priceRange.Min = Int32.Parse(value[0]);
                    priceRange.Max = Int32.Parse(value[1]);
                    priceRanges.Add(priceRange);
                }
                filterdProducts=filterdProducts.Where(p=>priceRanges.Any(r=>p.DonGia>=r.Min&&p.DonGia<=r.Max)).ToList();
            }
            // color
           else if (filter.Colors != null && filter.Colors.Count > 0 && !filter.Colors.Contains("all"))
            {
                filterdProducts = filterdProducts.Where(p => filter.Colors.Contains(p.MauSacs.Ten)).ToList();
            }
            //size
          else  if (filter.Sizes != null && filter.Sizes.Count > 0 && !filter.Sizes.Contains("all"))
            {
                filterdProducts = filterdProducts.Where(p => filter.Sizes.Contains(p.Sizes.Ten)).ToList();
            }
            return PartialView("SanPhamSearch",filterdProducts);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
      
        public async Task<IActionResult> Create(SanPham sanpham)
        {
            var url = $"https://localhost:7016/api/SanPham?Ten={sanpham.Ten}&MoTa={sanpham.MoTa}&TrangThai={sanpham.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllSanPham");
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        {
            //List Anh
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //List MauSac
            string urlMauSac = "https://localhost:7016/api/MauSac";
            var responseMauSac = await httpClients.GetAsync(urlMauSac);
            string DataMauSac = await responseMauSac.Content.ReadAsStringAsync();
            var MauSac = JsonConvert.DeserializeObject<List<MauSac>>(DataMauSac);
            ViewBag.MauSac = MauSac;
            //List Size
            string urlSize = "https://localhost:7016/api/Size";
            var responseSize = await httpClients.GetAsync(urlSize);
            string DataSize = await responseSize.Content.ReadAsStringAsync();
            var Size = JsonConvert.DeserializeObject<List<Size>>(DataSize);
            ViewBag.Size = Size;
            // List SanPham
            string urlSanPham = "https://localhost:7016/api/SanPham";
            var responseSanPham = await httpClients.GetAsync(urlSanPham);
            string DataSanPham = await responseAnh.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(DataSanPham);
            ViewBag.SanPham = SanPham;
            string apiUrlct = "https://localhost:7016/api/ChiTietSP/" + id;
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<ChiTietSanPham>(apiDatact);
            ViewBag.ChiTietSanPham= CTSanPhams;
            return View(CTSanPhams);
            HttpContext.Session.SetString("IdSanPham", CTSanPhams.Id.ToString());
        }
        [HttpGet]
        public async Task<IActionResult> LoadTheoMauSac(Guid Idmausac)
        {
            Guid id= Guid.Parse(HttpContext.Session.GetString("IdSanPham"));
            string apiUrlct = $"https://localhost:7016/api/ChiTietSP/TimMauSac?id={id}&Idmausac={Idmausac}";
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<ChiTietSanPham>(apiDatact);
            return RedirectToAction("Details",id);
        }

        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7016/api/SanPham/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var SanPhams = JsonConvert.DeserializeObject<SanPham>(result);
            return View(SanPhams);
        }
        [HttpPost]
      
        public async Task<IActionResult> Update(SanPham sanpham)
        {

            var url =
                $"https://localhost:7016/api/SanPham/{sanpham.Id}?Ten={sanpham.Ten}&MoTa={sanpham.MoTa}&TrangThai={sanpham.TrangThai}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllSanPham");
            return View();
        }
      
    
        public async Task<IActionResult> Deletes(Guid id)
        {
            var url = $"https://localhost:7016/api/SanPham/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllSanPham");
            return BadRequest();
        }
    }
}
