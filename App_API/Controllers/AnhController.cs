using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnhController : ControllerBase
    {
        private readonly IAllRepositories<Anh> anhs;
        AppDbcontext context= new AppDbcontext();
        public AnhController()
        {
            anhs= new AllRepositories<Anh>(context,context.Anhs);
        }
        // GET: api/<AnhController>
        [HttpGet]
        public List<Anh> Get()
        {
            return anhs.GetAll();
        }

        // GET api/<AnhController>/5
        [HttpGet("{id}")]
        public Anh Get(Guid id)
        {
            return anhs.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<AnhController>
        [HttpPost]
        public bool Post(string Ten, int TrangThai )
        {
            Anh anh = new Anh();
            anh.Id=Guid.NewGuid();
            anh.Ten=Ten;
            anh.TrangThai=TrangThai;
            return anhs.Add(anh);
        }

        // PUT api/<AnhController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string Ten, int TrangThai)
        {
            var anh= anhs.GetAll().FirstOrDefault(x => x.Id == id);
            if(anh!=null)
            {
                anh.Ten = Ten;
                anh.TrangThai = TrangThai;
                return anhs.Update(anh);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<AnhController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var anh = anhs.GetAll().FirstOrDefault(x => x.Id == id);
            if (anh != null)
            {
              
                return anhs.Delete(anh);
            }
            else
            {
                return false;
            }
        }
    }
}
