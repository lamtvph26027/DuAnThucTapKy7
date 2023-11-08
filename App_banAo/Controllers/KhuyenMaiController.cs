using App_banAo.Models;
using App_data.Models;
using App_data.ViewModels;
using App_data.ViewModels.ChiTietSPView;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Drawing.Printing;

namespace App_banAo.Controllers
{
    public class KhuyenMaiController : Controller
    {
        private readonly HttpClient httpClients;

        public KhuyenMaiController()
        {
            httpClients = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }
        public int PageSize = 8;
        [HttpGet]

        public async Task<IActionResult> GetAllKhuyenMai(int ProductPage = 1)
        {   
            //list km
            string apiUrl = "https://localhost:7016/api/KhuyenMai";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData);
            ViewBag.KhuyenMaiView=KhuyenMais;
            
           

                return View(/*CTSanPhams*/

                    new PhanTrangKhuyenMai
                    {
                       listkmv = KhuyenMais
                        .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                        PagingInfo = new PagingInfo
                        {
                            ItemsPerPage = PageSize,
                            CurrentPage = ProductPage,
                            TotalItems = KhuyenMais.Count()
                        }

                    }
                    );
            
            
        }
        [HttpGet]

        public async Task<IActionResult> Search(string Ten,int ProductPage = 1)
        {
            //list km
            string apiUrl = "https://localhost:7016/api/KhuyenMai";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData);
            ViewBag.KhuyenMaiView = KhuyenMais;



            return View("GetAllKhuyenMai",

                new PhanTrangKhuyenMai
                {
                    listkmv = KhuyenMais.Where(x=>x.Ten.Contains(Ten))
                    .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = ProductPage,
                        TotalItems = KhuyenMais.Count()
                    }

                }
                );


        }
        [HttpGet]

        public async Task<IActionResult> GetAllSPByKM(Guid id,int ProductPage = 1)
        {

            // list anh 
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //list km
            string apiUrl = "https://localhost:7016/api/KhuyenMai";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData);
            ViewBag.KhuyenMaiView = KhuyenMais;
            // luu idkhm vo session
            var KhuyenMai = KhuyenMais.FirstOrDefault(x => x.Id == id);

            HttpContext.Session.SetString("IdKhuyenMai", KhuyenMai.Id.ToString());
            // list lsp
            string apiUrllsp = "https://localhost:7016/api/LoaiSP";
            var responselsp = await httpClients.GetAsync(apiUrllsp);
            string apiDatalsp = await responselsp.Content.ReadAsStringAsync();
            var LoaiSPs = JsonConvert.DeserializeObject<List<LoaiSP>>(apiDatalsp);
            ViewBag.LoaiSPs = LoaiSPs;
            // list allspby km
            string apiUrl1 = $"https://localhost:7016/api/KhuyenMai/GetAllSPByKm?id={id}";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1= await response1.Content.ReadAsStringAsync();
            var KhuyenMais1 = JsonConvert.DeserializeObject<List<AllSanPhamByKM>>(apiData1);


            return View(/*CTSanPhams*/
                
                new PhanTrangSanPhamByKM
                {
                    listsp = KhuyenMais1
                    .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = ProductPage,
                        TotalItems = KhuyenMais1.Count()
                    }

                }
                );


        }
        [HttpGet]

        public async Task<IActionResult> GetAllSPNoKM( int ProductPage = 1)
        {

            // list anh 
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //list km
            string apiUrl = "https://localhost:7016/api/KhuyenMai";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData);
            ViewBag.KhuyenMaiView = KhuyenMais;
            // list lsp
            string apiUrllsp = "https://localhost:7016/api/LoaiSP";
            var responselsp = await httpClients.GetAsync(apiUrllsp);
            string apiDatalsp = await responselsp.Content.ReadAsStringAsync();
            var LoaiSPs = JsonConvert.DeserializeObject<List<LoaiSP>>(apiDatalsp);
            ViewBag.LoaiSPs = LoaiSPs;
            // list allspby km
            // lay idkm tu session
            var idkhuyenmai = Guid.Parse(HttpContext.Session.GetString("IdKhuyenMai"));
            string apiUrl1 = $"https://localhost:7016/api/KhuyenMai/GetAllSPNoKm?id={idkhuyenmai}";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais1 = JsonConvert.DeserializeObject<List<AllSanPhamByKM>>(apiData1);


            return View(/*CTSanPhams*/

                new PhanTrangSanPhamByKM
                {
                    listsp = KhuyenMais1
                    .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = ProductPage,
                        TotalItems = KhuyenMais1.Count()
                    }

                }
                );


        }
        [HttpGet]
        public async Task<IActionResult> LocLSPGetAllSPNoKM(Guid id,int ProductPage = 1)
        {

            // list anh 
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //list km
            string apiUrl = "https://localhost:7016/api/KhuyenMai";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData);
            ViewBag.KhuyenMaiView = KhuyenMais;
            // list lsp
            string apiUrllsp = "https://localhost:7016/api/LoaiSP";
            var responselsp = await httpClients.GetAsync(apiUrllsp);
            string apiDatalsp = await responselsp.Content.ReadAsStringAsync();
            var LoaiSPs = JsonConvert.DeserializeObject<List<LoaiSP>>(apiDatalsp);
            ViewBag.LoaiSPs = LoaiSPs;
            // list allspby km
            // lay idkm tu session
            var idkhuyenmai = Guid.Parse(HttpContext.Session.GetString("IdKhuyenMai"));
            string apiUrl1 = $"https://localhost:7016/api/KhuyenMai/GetAllSPNoKm?id={idkhuyenmai}";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var KhuyenMais1 = JsonConvert.DeserializeObject<List<AllSanPhamByKM>>(apiData1);
            

            return View(/*CTSanPhams*/

                new PhanTrangSanPhamByKM
                {
                    listsp = KhuyenMais1.Where(x=>x.IdLoaiSP==id)
                    .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = ProductPage,
                        TotalItems = KhuyenMais1.Count()
                    }

                }
                );


        }
        [HttpGet]
        public async Task<IActionResult> AddKHuyenMaiVoSP(Guid id,int ProductPage = 1)
        {

            // list anh 
            string urlAnh = "https://localhost:7016/api/Anh";
            var responseAnh = await httpClients.GetAsync(urlAnh);
            string DataAnh = await responseAnh.Content.ReadAsStringAsync();
            var Anh = JsonConvert.DeserializeObject<List<Anh>>(DataAnh);
            ViewBag.Anh = Anh;
            //list km
            string apiUrl = "https://localhost:7016/api/KhuyenMai";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<List<KhuyenMaiView>>(apiData);
            ViewBag.KhuyenMaiView = KhuyenMais;
            //list mausac
            string apiUrlms = "https://localhost:7016/api/MauSac";
            var responsems = await httpClients.GetAsync(apiUrlms);
            string apiDatams = await responsems.Content.ReadAsStringAsync();
            var MauSacs = JsonConvert.DeserializeObject<List<MauSac>>(apiDatams);
            ViewBag.MauSac = MauSacs;
            //list size
            string apiUrlsize = "https://localhost:7016/api/Size";
            var responsesize = await httpClients.GetAsync(apiUrlsize);
            string apiDatasize = await responsesize.Content.ReadAsStringAsync();
            var Sizes = JsonConvert.DeserializeObject<List<Size>>(apiDatasize);
            ViewBag.Size = Sizes;
            // list SP
            string apiUrlsp = "https://localhost:7016/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPhams = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPhams;
            //list ctsp
            string apiUrlct = "https://localhost:7016/api/ChiTietSP";
            var responsect = await httpClients.GetAsync(apiUrlct);
            string apiDatact = await responsect.Content.ReadAsStringAsync();
            var CTSanPhams = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiDatact);
            ViewBag.ChiTietSanPham = CTSanPhams;


            //List AllChiTietSP
            string apiUrl1 = $"https://localhost:7016/api/ChiTietSPView/idSanPham?id={id}";
            var response1 = await httpClients.GetAsync(apiUrl1);
            string apiData1 = await response1.Content.ReadAsStringAsync();
            var AllCTSanPhams = JsonConvert.DeserializeObject<List<AllChiTietSanPham>>(apiData1);

            return View(/*CTSanPhams*/

                new ProductListViewModel
                {
                    chiTietSanPhams = AllCTSanPhams
                    .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = ProductPage,
                        TotalItems = AllCTSanPhams.Count()
                    }

                }
                );

        }
        [HttpPost]
        public async Task<IActionResult> AddKHuyenMaiVoSP( List<Guid> sanphams)
        {
            // lay idkhuyenmai từ Session 
            var idkhuyenmai = Guid.Parse(HttpContext.Session.GetString("IdKhuyenMai"));
            // QLkm
            var response3 = await httpClients.PutAsJsonAsync($"https://localhost:7016/api/ChiTietSPView/QLKhuyenMai?IdKhuyenMai={idkhuyenmai}", sanphams);
            if (response3.IsSuccessStatusCode) return Json(new { success = true, message = "Cập nhật thành công!" });
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteKHuyenMaiRaSP(List<Guid> sanphams)
        {
            // QLkm
            var response3 = await httpClients.PutAsJsonAsync($"https://localhost:7016/api/ChiTietSPView/QLKhuyenMaiDelete", sanphams);
            if (response3.IsSuccessStatusCode) return Json(new { success = true, message = "Cập nhật thành công!" });
            return Json(new { success = false });
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(KhuyenMaiView khuyenMai)
        {
           

           
            var response = await httpClients.PostAsJsonAsync("https://localhost:7016/api/KhuyenMai", khuyenMai);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllKhuyenMai");
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        {

            string apiUrl = "https://localhost:7016/api/KhuyenMai/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var KhuyenMais = JsonConvert.DeserializeObject<KhuyenMaiView>(apiData);
            return View(KhuyenMais);

        }

        // add khuyen mai vo ctsp
        [HttpGet]

       

        public IActionResult Update(Guid id)
        {
           
            var url = $"https://localhost:7016/api/KhuyenMai/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var KhuyenMais = JsonConvert.DeserializeObject<KhuyenMaiView>(result);
            return View(KhuyenMais);
        }
        [HttpPost]

        public async Task<IActionResult> Update(KhuyenMaiView khuyenMai)
        {

            DateTime NgayApDung=khuyenMai.NgayApDung;
            DateTime NgayKetThuc=khuyenMai.NgayKetThuc;
            var response = await httpClients.PutAsJsonAsync($"https://localhost:7016/api/KhuyenMai/{khuyenMai.Id}", khuyenMai);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllKhuyenMai");
           return View();
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var url = $"https://localhost:7016/api/KhuyenMai/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllKhuyenMai");
            return BadRequest();
        }
    }
}
