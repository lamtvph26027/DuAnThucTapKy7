using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly IAllRepositories<Size> sizes;
        AppDbcontext context = new AppDbcontext();
        public SizeController()
        {
            sizes = new AllRepositories<Size>(context, context.Sizes);
        }
        // GET: api/<SizeController>
        [HttpGet]
        public List<Size> Get()
        {
            return sizes.GetAll();
        }

        // GET api/<SizeController>/5
        [HttpGet("{id}")]
        public Size Get(Guid id)
        {
            return sizes.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<SizeController>
        [HttpPost]
        public bool Post(string Ten, int TrangThai)
        {
           Size size = new Size();
            size.Id = Guid.NewGuid();
            size.Ten = Ten;
            size.TrangThai = TrangThai;
            return sizes.Add(size);
        }

        // PUT api/<SizeController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string Ten, int TrangThai)
        {
            var size = sizes.GetAll().FirstOrDefault(c => c.Id == id);
            if (size != null)
            {
                size.Ten = Ten;
                size.TrangThai = TrangThai;
                return sizes.Update(size);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<SizeController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var size = sizes.GetAll().FirstOrDefault(c => c.Id == id);
            if (size != null)
            {

                return sizes.Delete(size);
            }
            else
            {
                return false;
            }
        }
    }
}
