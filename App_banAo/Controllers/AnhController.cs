using App_data.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App_banAo.Controllers
{
    public class AnhController : Controller
    {
        
      
        private readonly HttpClient httpClients;
       

        public AnhController()
        {
            httpClients = new HttpClient();
           
        }


        [HttpGet]

        public async Task<IActionResult> GetAllAnh()
        {
           
            string apiUrl = "https://localhost:7016/api/Anh";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var Anhs = JsonConvert.DeserializeObject<List<Anh>>(apiData);
            return View(Anhs);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create([Bind]IFormFile formFile, Anh anh)
        {
            if (formFile != null && formFile.Length > 0) // Kiểm tra đường dẫn phù hợp
            {
                //check ảnh tồn tại trong folder chưa
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","userassets",
                    "img", formFile.FileName);
                if (!System.IO.File.Exists(path))
                {
                    // thực hiện việc sao chép ảnh đó vào wwwroot
                    // Tạo đường dẫn tới thư mục sao chép (nằm trong root)
                    // abc/wwwroot/images/xxx.
                    var stream = new FileStream(path, FileMode.Create); // Tạo 1 filestream để tạo mới
                    formFile.CopyTo(stream); // Copy ảnh vừa dc chọn vào đúng cái stream đó

                    // Gán lại giá trị link ảnh (lúc này đã nằm trong root cho thuộc tính description)
                    anh.Ten = formFile.FileName;
                }
                else
                {
                    anh.Ten = formFile.FileName;
                }

            }
            var url = $"https://localhost:7016/api/Anh?Ten={anh.Ten}&TrangThai={anh.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllAnh");
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        {

            string apiUrl = "https://localhost:7016/api/Anh/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var Anhs = JsonConvert.DeserializeObject<Anh>(apiData);
            return View(Anhs);

        }

        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7016/api/Anh/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var Anhs = JsonConvert.DeserializeObject<Anh>(result);
            return View(Anhs);
        }
        [HttpPost]

        public async Task<IActionResult> Update(Anh anh,[Bind]IFormFile formFile)
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
                    anh.Ten = formFile.FileName;
                }
                else
                {
                    anh.Ten = formFile.FileName;
                }

            }
            var url =
                $"https://localhost:7016/api/Anh/{anh.Id}?Ten={anh.Ten}&TrangThai={anh.TrangThai}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllAnh");
            return View();
        }


        public async Task<IActionResult> Deletes(Guid id)
        {
            var url = $"https://localhost:7016/api/Anh/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllAnh");
            return BadRequest();
        }
    }
}

