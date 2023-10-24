using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietPTTTController : ControllerBase 
    {
        private readonly IAllRepositories<ChiTietPhuongThucThanhToan> chitietpttts;
        AppDbcontext context= new AppDbcontext();
        public ChiTietPTTTController()
        {
            chitietpttts = new AllRepositories<ChiTietPhuongThucThanhToan>(context,context.ChiTietPTTTs);
        }
        // GET: api/<ChiTietPTTTController>
        [HttpGet]
        public List<ChiTietPhuongThucThanhToan> Get()
        {
            return chitietpttts.GetAll();
        }

        // GET api/<ChiTietPTTTController>/5
        [HttpGet("{id}")]
        public ChiTietPhuongThucThanhToan Get(Guid id)
        {
            return chitietpttts.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<ChiTietPTTTController>
        [HttpPost]
        public bool Post(Guid IdPTTT,Guid IdHoaDon,int sotien,int TrangThai)
        {
            ChiTietPhuongThucThanhToan chitietpttt = new ChiTietPhuongThucThanhToan();
            chitietpttt.Id = Guid.NewGuid();
            chitietpttt.IdPTTT = IdPTTT;
            chitietpttt.IdHoaDon = IdHoaDon;
            chitietpttt.SoTien = sotien;
            chitietpttt.TrangThai = TrangThai;
            return chitietpttts.Add(chitietpttt);
        }

        // PUT api/<ChiTietPTTTController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid IdPTTT, Guid IdHoaDon, int sotien, int TrangThai)
        {
            var chitietpttt= chitietpttts.GetAll().FirstOrDefault(x => x.Id == id);
            if (chitietpttt != null)
            {
                chitietpttt.IdPTTT = IdPTTT;
                chitietpttt.IdHoaDon = IdHoaDon;
                chitietpttt.SoTien = sotien;
                chitietpttt.TrangThai = TrangThai;
                return chitietpttts.Update(chitietpttt);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<ChiTietPTTTController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var chitietpttt = chitietpttts.GetAll().FirstOrDefault(x => x.Id == id);
            if (chitietpttt != null)
            {
                
                return chitietpttts.Delete(chitietpttt);
            }
            else
            {
                return false;
            }
        }
    }
}
