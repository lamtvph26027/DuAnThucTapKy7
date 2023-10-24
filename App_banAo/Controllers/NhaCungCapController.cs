using App_data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App_banAo.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly HttpClient httpClients;

        public NhaCungCapController()
        {
            httpClients = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllNhaCungCap()
        {
            string apiUrl = "https://localhost:7016/api/NhaCungCap";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var NhaCungCaps = JsonConvert.DeserializeObject<List<NhaCungCap>>(apiData);
            return View(NhaCungCaps);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(NhaCungCap nhaCungCap)
        {
            var url = $"https://localhost:7016/api/NhaCungCap?Ten={nhaCungCap.Ten}&TrangThai={nhaCungCap.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllNhaCungCap");
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        {

            string apiUrl = "https://localhost:7016/api/NhaCungCap/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var NhaCungCaps = JsonConvert.DeserializeObject<NhaCungCap>(apiData);
            return View(NhaCungCaps);

        }

        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7016/api/NhaCungCap/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var NhaCungCaps = JsonConvert.DeserializeObject<NhaCungCap>(result);
            return View(NhaCungCaps);
        }
        [HttpPost]

        public async Task<IActionResult> Update(NhaCungCap nhaCungCap)
        {

            var url =
                $"https://localhost:7016/api/NhaCungCap/{nhaCungCap.Id}?Ten={nhaCungCap.Ten}&TrangThai= {nhaCungCap.TrangThai}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllNhaCungCap");


            return View();
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var url = $"https://localhost:7016/api/NhaCungCap/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllNhaCungCap");
            return BadRequest();
        }
    }
}
