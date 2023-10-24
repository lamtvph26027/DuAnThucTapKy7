using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiSPController : ControllerBase
    {
        private readonly IAllRepositories<LoaiSP> loaisps;
        AppDbcontext context= new AppDbcontext();
        public LoaiSPController()
        {
            loaisps= new AllRepositories<LoaiSP>(context,context.LoaiSPs);
        }
        // GET: api/<LoaiSPController>
        [HttpGet]
        public List<LoaiSP> Get()
        {
            return loaisps.GetAll();
        }

        // GET api/<LoaiSPController>/5
        [HttpGet("{id}")]
        public LoaiSP Get(Guid id)
        {
            return loaisps.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<LoaiSPController>
        [HttpPost]
        public bool Post(string Ten, int TrangThai, Guid? IdLoaiSPCha, Guid IdChatLieu)
        {
            LoaiSP loaiSP = new LoaiSP();
            loaiSP.Id=Guid.NewGuid();
            loaiSP.Ten=Ten;
            loaiSP.TrangThai=TrangThai;
            loaiSP.IdLoaiSPCha=IdLoaiSPCha;
            loaiSP.IdChatLieu=IdChatLieu;
            return loaisps.Add(loaiSP);
        }

        // PUT api/<LoaiSPController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string Ten, int TrangThai, Guid? IdLoaiSPCha, Guid IdChatLieu)
        {
            var loaiSP = loaisps.GetAll().FirstOrDefault(x => x.Id == id);
            if (loaiSP != null)
            {
                loaiSP.Ten = Ten;
                loaiSP.TrangThai = TrangThai;
                loaiSP.IdLoaiSPCha = IdLoaiSPCha;
                loaiSP.IdChatLieu = IdChatLieu;
                return loaisps.Update(loaiSP);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<LoaiSPController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var loaiSP = loaisps.GetAll().FirstOrDefault(x => x.Id == id);
            if (loaiSP != null)
            {
                
                return loaisps.Delete(loaiSP);
            }
            else
            {
                return false;
            }
        }
    }
}
