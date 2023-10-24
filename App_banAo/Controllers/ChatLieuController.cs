using App_data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App_banAo.Controllers
{
    public class ChatLieuController : Controller
    {
        private readonly HttpClient httpClients;

        public ChatLieuController()
        {
            httpClients = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllChatLieu()
        {
            string apiUrl = "https://localhost:7016/api/ChatLieu";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var ChatLieus = JsonConvert.DeserializeObject<List<ChatLieu>>(apiData);
            return View(ChatLieus);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(ChatLieu chatLieu)
        {
            var url = $"https://localhost:7016/api/ChatLieu?Ten={chatLieu.Ten}&TrangThai={chatLieu.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllChatLieu");
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        {

            string apiUrl = "https://localhost:7016/api/ChatLieu/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var ChatLieus = JsonConvert.DeserializeObject<ChatLieu>(apiData);
            return View(ChatLieus);

        }

        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7016/api/ChatLieu/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var ChatLieus = JsonConvert.DeserializeObject<ChatLieu>(result);
            return View(ChatLieus);
        }
        [HttpPost]

        public async Task<IActionResult> Update(ChatLieu chatLieu)
        {

            var url =
                $"https://localhost:7016/api/ChatLieu/{chatLieu.Id}?Ten={chatLieu.Ten}&TrangThai= {chatLieu.TrangThai}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllChatLieu");


            return View();
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var url = $"https://localhost:7016/api/ChatLieu/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllChatLieu");
            return BadRequest();
        }
    }
}
