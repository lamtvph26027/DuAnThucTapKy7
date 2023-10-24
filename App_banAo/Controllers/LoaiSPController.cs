using App_data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App_banAo.Controllers
{
    public class LoaiSPController : Controller
    {
        private readonly HttpClient httpClients;

        public LoaiSPController()
        {
            httpClients = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllLoaiSP()
        {
            string apiUrl = "https://localhost:7016/api/LoaiSP";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var LoaiSPs = JsonConvert.DeserializeObject<List<LoaiSP>>(apiData);
            return View(LoaiSPs);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(LoaiSP loaiSP)
        {
            var url = $"https://localhost:7016/api/LoaiSP?Ten={loaiSP.Ten}&TrangThai={loaiSP.TrangThai}&IdLoaiSPCha={loaiSP.IdLoaiSPCha}&IDChatLieu={loaiSP.IdChatLieu}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllLoaiSP");
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        {

            string apiUrl = "https://localhost:7016/api/LoaiSP/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var LoaiSPs = JsonConvert.DeserializeObject<LoaiSP>(apiData);
            return View(LoaiSPs);

        }

        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7016/api/LoaiSP/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var LoaiSPs = JsonConvert.DeserializeObject<LoaiSP>(result);
            return View(LoaiSPs);
        }
        [HttpPost]

        public async Task<IActionResult> Update(LoaiSP loaiSP)
        {

            var url = $"https://localhost:7016/api/LoaiSP/{loaiSP.Id}?Ten={loaiSP.Ten}&TrangThai={loaiSP.TrangThai}&IdChatLieu={loaiSP.IdChatLieu}&IdSPhCha={loaiSP.IdLoaiSPCha}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllLoaiSP");


            return View();
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var url = $"https://localhost:7016/api/LoaiSP/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllLoaiSP");
            return BadRequest();
        }
    }
}
