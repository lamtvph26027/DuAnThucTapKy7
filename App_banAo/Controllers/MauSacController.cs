using App_data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App_banAo.Controllers
{
    public class MauSacController : Controller
    {
        private readonly HttpClient httpClients;


        public MauSacController()
        {
            httpClients = new HttpClient();

        }


        [HttpGet]

        public async Task<IActionResult> GetAllMauSac()
        {

            string apiUrl = "https://localhost:7016/api/MauSac";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var MauSacs = JsonConvert.DeserializeObject<List<MauSac>>(apiData);
            return View(MauSacs);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create([Bind] IFormFile formFile, MauSac MauSac)
        {
            if (formFile != null && formFile.Length > 0) // Kiểm tra đường dẫn phù hợp
            {
                //check ảnh tồn tại trong folder chưa
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "userassets",
                    "img", formFile.FileName);
                if (!System.IO.File.Exists(path))
                {
                    // thực hiện việc sao chép ảnh đó vào wwwroot
                    // Tạo đường dẫn tới thư mục sao chép (nằm trong root)
                    // abc/wwwroot/images/xxx.
                    var stream = new FileStream(path, FileMode.Create); // Tạo 1 filestream để tạo mới
                    formFile.CopyTo(stream); // Copy ảnh vừa dc chọn vào đúng cái stream đó

                    // Gán lại giá trị link ảnh (lúc này đã nằm trong root cho thuộc tính description)
                    MauSac.AnhMauSac = formFile.FileName;
                }
                else
                {
                    MauSac.AnhMauSac = formFile.FileName;
                }

            }
        
            var url = $"https://localhost:7016/api/MauSac?Ten={MauSac.Ten}&TrangThai={MauSac.TrangThai}&AnhMauSac={MauSac.AnhMauSac}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllMauSac");
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        {

            string apiUrl = "https://localhost:7016/api/MauSac/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var MauSacs = JsonConvert.DeserializeObject<MauSac>(apiData);
            return View(MauSacs);

        }

        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7016/api/MauSac/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var MauSacs = JsonConvert.DeserializeObject<MauSac>(result);
            return View(MauSacs);
        }
        [HttpPost]

        public async Task<IActionResult> Update(MauSac MauSac, [Bind] IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0) // Kiểm tra đường dẫn phù hợp
            {
                //check ảnh tồn tại trong folder chưa
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "userassets",
                    "img", formFile.FileName);
                if (!System.IO.File.Exists(path))
                {
                    // thực hiện việc sao chép ảnh đó vào wwwroot
                    // Tạo đường dẫn tới thư mục sao chép (nằm trong root)
                    // abc/wwwroot/images/xxx.
                    var stream = new FileStream(path, FileMode.Create); // Tạo 1 filestream để tạo mới
                    formFile.CopyTo(stream); // Copy ảnh vừa dc chọn vào đúng cái stream đó

                    // Gán lại giá trị link ảnh (lúc này đã nằm trong root cho thuộc tính description)
                    MauSac.AnhMauSac = formFile.FileName;
                }
                else
                {
                    MauSac.AnhMauSac = formFile.FileName;
                }

            }
            var url =
                $"https://localhost:7016/api/MauSac/{MauSac.Id}?Ten={MauSac.Ten}&TrangThai={MauSac.TrangThai}&AnhMauSac={MauSac.AnhMauSac}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllMauSac");
            return View();
        }


        public async Task<IActionResult> Deletes(Guid id)
        {
            var url = $"https://localhost:7016/api/MauSac/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllMauSac");
            return BadRequest();
        }
    }
}
