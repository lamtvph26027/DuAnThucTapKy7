using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly IAllRepositories<SanPham> sanphams;
        AppDbcontext context= new AppDbcontext();
        public SanPhamController()
        {
            sanphams= new AllRepositories<SanPham>(context,context.SanPhams);
        }
        // GET: api/<SanPhamController>
        [HttpGet]
        public List<SanPham> Get()
        {
            return sanphams.GetAll();
        }

        // GET api/<SanPhamController>/5
        [HttpGet("{id}")]
        public SanPham Get(Guid id)
        {
            return sanphams.GetAll().FirstOrDefault(x => x.Id == id);
        }
        [Route("ten")]
        [HttpGet]
        public List<SanPham> GetById(string ten)
        {
            return sanphams.GetAll().Where(x => x.Ten.Contains(ten)).ToList();
        }
        // POST api/<SanPhamController>
        [HttpPost]
        public bool Post(string Ten,string MoTa,int TrangThai)
        {
            SanPham sanpham= new SanPham();
            sanpham.Id=Guid.NewGuid();
            sanpham.Ten=Ten;
            sanpham.MoTa=MoTa;
          
            sanpham.TrangThai=TrangThai;
          
            return sanphams.Add(sanpham);
        }

        // PUT api/<SanPhamController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string Ten, string MoTa, int TrangThai)
        {
            var sanpham= sanphams.GetAll().FirstOrDefault(x => x.Id == id);
            if (sanpham != null)
            {
                sanpham.Ten = Ten;
                sanpham.MoTa = MoTa;
              
                sanpham.TrangThai = TrangThai;
               
                return sanphams.Update(sanpham);
            }
            else
            {
                return false;   
            }
        }

        // DELETE api/<SanPhamController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var sanpham = sanphams.GetAll().FirstOrDefault(x => x.Id == id);
            if (sanpham != null)
            {
               
                return sanphams.Delete(sanpham);
            }
            else
            {
                return false;
            }
        }
    }
}
