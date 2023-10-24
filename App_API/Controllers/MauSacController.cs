using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MauSacController : ControllerBase
    {
        private readonly IAllRepositories<MauSac> mausacs;
        AppDbcontext context= new AppDbcontext();
        public MauSacController()
        {
            mausacs = new AllRepositories<MauSac>(context,context.MauSacs);
        }
        // GET: api/<MauSacController>
        [HttpGet]
        public List<MauSac> Get()
        {
            return mausacs.GetAll();
        }

        // GET api/<MauSacController>/5
        [HttpGet("{id}")]
        public MauSac Get(Guid id)
        {
            return mausacs.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<MauSacController>
        [HttpPost]
        public bool Post(string Ten, int TrangThai,string? AnhMauSac)
        {
           MauSac mausac = new MauSac();
            mausac.Id = Guid.NewGuid();
            mausac.Ten = Ten;
            mausac.TrangThai = TrangThai;
            mausac.AnhMauSac = AnhMauSac;
            return mausacs.Add(mausac);
        }

        // PUT api/<MauSacController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string Ten, int TrangThai ,string? AnhMauSac)
        {
            var mausac = mausacs.GetAll().FirstOrDefault(c => c.Id == id);
            if (mausac != null)
            {
                mausac.Ten = Ten;
                mausac.AnhMauSac = AnhMauSac;
                mausac.TrangThai = TrangThai;
                return mausacs.Update(mausac);
            }
            else
            {
                return false;

            }
        }
                // DELETE api/<MauSacController>/5
                [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var mausac = mausacs.GetAll().FirstOrDefault(c => c.Id == id);
            if (mausac != null)
            {

                return mausacs.Delete(mausac);
            }
            else
            {
                return false;
            }
        }
    }
}
