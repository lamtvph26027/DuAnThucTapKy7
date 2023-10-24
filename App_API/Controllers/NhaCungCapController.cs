using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapController : ControllerBase
    {
        private readonly IAllRepositories<NhaCungCap> nhacungcaps;
        AppDbcontext context= new AppDbcontext();
        public NhaCungCapController()
        {
            nhacungcaps = new AllRepositories<NhaCungCap>(context,context.NhaCungCaps);
        }
        // GET: api/<NhaCungCapController>
        [HttpGet]
        public List<NhaCungCap> Get()
        {
            return nhacungcaps.GetAll();
        }

        // GET api/<NhaCungCapController>/5
        [HttpGet("{id}")]
        public NhaCungCap Get(Guid id)
        {
            return nhacungcaps.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<NhaCungCapController>
        [HttpPost]
        public bool Post(string Ten, int TrangThai)
        {
           NhaCungCap nhacungcap = new NhaCungCap();
            nhacungcap.Id = Guid.NewGuid();
            nhacungcap.Ten = Ten;
            nhacungcap.TrangThai = TrangThai;
            return nhacungcaps.Add(nhacungcap);
        }

        // PUT api/<NhaCungCapController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string Ten, int TrangThai)
        {
            var nhacungcap = nhacungcaps.GetAll().FirstOrDefault(c => c.Id == id);
            if (nhacungcap != null)
            {
                nhacungcap.Ten = Ten;
                nhacungcap.TrangThai = TrangThai;
                return nhacungcaps.Update(nhacungcap);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<NhaCungCapController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var nhacungcap = nhacungcaps.GetAll().FirstOrDefault(c => c.Id == id);
            if (nhacungcap != null)
            {

                return nhacungcaps.Delete(nhacungcap);
            }
            else
            {
                return false;
            }
        }
    }
}
