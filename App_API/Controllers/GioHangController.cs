using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly IAllRepositories<GioHang> giohangs;
        AppDbcontext context= new AppDbcontext();
        public GioHangController()
        {
            giohangs = new AllRepositories<GioHang>(context,context.GioHangs);
        }
        // GET: api/<GioHangController>
        [HttpGet]
        public List<GioHang> Get()
        {
            return giohangs.GetAll();
        }

        // GET api/<GioHangController>/5
        [HttpGet("{id}")]
        public GioHang Get(Guid id)
        {
            return giohangs.GetAll().FirstOrDefault(x => x.IdNguoiDung == id);
        }

        // POST api/<GioHangController>
        [HttpPost]
        public bool Post(Guid IdNguoiDung)
        {
            GioHang gioHang = new GioHang();
            gioHang.IdNguoiDung = IdNguoiDung;
            gioHang.NgayTao = DateTime.Now;
            return giohangs.Add(gioHang);
        }

        // PUT api/<GioHangController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] string value)
        {

        }

        // DELETE api/<GioHangController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var giohang= giohangs.GetAll().FirstOrDefault(x => x.IdNguoiDung == id);
            if(giohang != null)
            {
                return giohangs.Delete(giohang);
            }
            else
            {
                return false;
            }
        }
    }
}
