using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangThaiHoaDonController : ControllerBase
    {
        private readonly IAllRepositories<TrangThaiHoaDon> trangthais;
        AppDbcontext context = new AppDbcontext();
        public TrangThaiHoaDonController()
        {
            trangthais=new AllRepositories<TrangThaiHoaDon>(context,context.TrangThaiHoaDons);
        }
        // GET: api/<TrangThaiHoaDonController>
        [HttpGet]
        public List<TrangThaiHoaDon> Get()
        {
            return trangthais.GetAll();
        }

        // GET api/<TrangThaiHoaDonController>/5
        [HttpGet("{id}")]
        public TrangThaiHoaDon Get(Guid id)
        {
            return trangthais.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<TrangThaiHoaDonController>
        [HttpPost]
        public bool Post(string Ten, int TrangThai)
        {
           TrangThaiHoaDon trangthai = new TrangThaiHoaDon();
            trangthai.Id = Guid.NewGuid();
            trangthai.Ten = Ten;
            trangthai.TrangThai = TrangThai;
            return trangthais.Add(trangthai);
        }

        // PUT api/<TrangThaiHoaDonController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string Ten, int TrangThai)
        {
            var trangthai = trangthais.GetAll().FirstOrDefault(c => c.Id == id);
            if (trangthai != null)
            {
                trangthai.Ten = Ten;
                trangthai.TrangThai = TrangThai;
                return trangthais.Update(trangthai);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<TrangThaiHoaDonController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var trangthai = trangthais.GetAll().FirstOrDefault(c => c.Id == id);
            if (trangthai != null)
            {

                return trangthais.Delete(trangthai);
            }
            else
            {
                return false;
            }
        }
    }
}
