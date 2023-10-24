using App_data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App_banAo.Controllers
{
    public class SizeController : Controller
    {
         private readonly HttpClient httpClients;

        public SizeController()
        {
            httpClients = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllSize()
        {
            string apiUrl = "https://localhost:7016/api/Size";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var Sizes = JsonConvert.DeserializeObject<List<Size>>(apiData);
            return View(Sizes);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Size size)
        {
            var url = $"https://localhost:7016/api/Size?Ten={size.Ten}&TrangThai={size.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllSize");
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        {

            string apiUrl = "https://localhost:7016/api/Size/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var Sizes = JsonConvert.DeserializeObject<Size>(apiData);
            return View(Sizes);

        }

        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7016/api/Size/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var Sizes = JsonConvert.DeserializeObject<Size>(result);
            return View(Sizes);
        }
        [HttpPost]

        public async Task<IActionResult> Update(Size size)
        {

            var url = $"https://localhost:7016/api/Size/{size.Id}?Ten={size.Ten}&TrangThai={size.TrangThai}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllSize");


            return View();
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var url = $"https://localhost:7016/api/Size/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllSize");
            return BadRequest();
        }
    }
}
