using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroController : ControllerBase
    {
        private readonly IAllRepositories<VaiTro> vaitros;
        AppDbcontext context=new AppDbcontext();
        public VaiTroController()
        {
            vaitros = new AllRepositories<VaiTro>(context,context.VaiTros);
        }
        // GET: api/<VaiTroController>
        [HttpGet]
        public List<VaiTro> Get()
        {
           return vaitros.GetAll(); 
        }

        // GET api/<VaiTroController>/5
        [HttpGet("{id}")]
        public VaiTro Get(Guid id)
        {
            return vaitros.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<VaiTroController>
        [HttpPost]
        public bool Post(string Ten, int TrangThai)
        {
            VaiTro vaitro=new VaiTro();
            vaitro.Id = Guid.NewGuid();
            vaitro.Ten = Ten;
            vaitro.TrangThai = TrangThai;
            return vaitros.Add(vaitro);
        }

        // PUT api/<VaiTroController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id,string Ten, int TrangThai)
        {
            var vaitro = vaitros.GetAll().FirstOrDefault(c => c.Id == id);
            if(vaitro != null)
            {
                vaitro.Ten = Ten;
                vaitro.TrangThai = TrangThai;
                return vaitros.Update(vaitro);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<VaiTroController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var vaitro = vaitros.GetAll().FirstOrDefault(c => c.Id == id);
            if (vaitro != null)
            {
                
                return vaitros.Delete(vaitro);
            }
            else
            {
                return false;
            }
        }
    }
}
