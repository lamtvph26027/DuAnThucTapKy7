using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhuongThucThanhToanController : ControllerBase
    {
        private readonly IAllRepositories<PhuongThucThanhToan> pttts;
        AppDbcontext context=new AppDbcontext();
        public PhuongThucThanhToanController()
        {
            pttts= new AllRepositories<PhuongThucThanhToan>(context,context.PhuongThucThanhToans);
        }
        // GET: api/<PhuongThucThanhToanController>
        [HttpGet]
        public List<PhuongThucThanhToan> Get()
        {
            return pttts.GetAll();
        }

        // GET api/<PhuongThucThanhToanController>/5
        [HttpGet("{id}")]
        public PhuongThucThanhToan Get(Guid id)
        {
            return pttts.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<PhuongThucThanhToanController>
        [HttpPost]
        public bool Post(string Ten, int TrangThai)
        {
           PhuongThucThanhToan pttt = new PhuongThucThanhToan();
            pttt.Id = Guid.NewGuid();
            pttt.Ten = Ten;
            pttt.TrangThai = TrangThai;
            return pttts.Add(pttt);
        }

        // PUT api/<PhuongThucThanhToanController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string Ten, int TrangThai)
        {
            var pttt = pttts.GetAll().FirstOrDefault(c => c.Id == id);
            if (pttt != null)
            {
                pttt.Ten = Ten;
                pttt.TrangThai = TrangThai;
                return pttts.Update(pttt);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<PhuongThucThanhToanController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var pttt = pttts.GetAll().FirstOrDefault(c => c.Id == id);
            if (pttt != null)
            {

                return pttts.Delete(pttt);
            }
            else
            {
                return false;
            }
        }
    }
}
