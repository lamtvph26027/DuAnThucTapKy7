using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using App_data.ViewModels.ChiTietSPView;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuyenMaiController : ControllerBase
    {
        private readonly IAllRepositories<KhuyenMai> khuyenmais;
        AppDbcontext context= new AppDbcontext();
        public KhuyenMaiController()
        {
            khuyenmais= new AllRepositories<KhuyenMai>(context,context.KhuyenMais);
        }
        // GET: api/<KhuyenMaiController>
        [HttpGet]
        public List<KhuyenMai> Get()
        {
            return khuyenmais.GetAll();
        }

        // GET api/<KhuyenMaiController>/5
        [HttpGet("{id}")]
        public KhuyenMai Get(Guid id)
        {
            return khuyenmais.GetAll().FirstOrDefault(x=>x.Id==id)  ;
        }

        // POST api/<KhuyenMaiController>
        [HttpPost]
        public bool Post(KhuyenMaiView kmv)
        {   kmv.Id=Guid.NewGuid();
            KhuyenMai khuyenMai = new KhuyenMai();
            khuyenMai.Id=kmv.Id;
            khuyenMai.Ten = kmv.Ten;
            khuyenMai.GiaTri = kmv.GiaTri;
            khuyenMai.NgayApDung = kmv.NgayApDung;
            khuyenMai.NgayKetThuc=kmv.  NgayKetThuc;
            if(khuyenMai.NgayApDung>khuyenMai.NgayKetThuc
                )
            {
                return false;
            }
            khuyenMai.MoTa = kmv.MoTa;
            khuyenMai.TrangThai = kmv.TrangThai;
            return khuyenmais.Add(khuyenMai);
        }

        // PUT api/<KhuyenMaiController>/5
        [HttpPut("{id}")]
        public bool Put(KhuyenMaiView kmv)
        {
            var khuyenMai= khuyenmais.GetAll().FirstOrDefault(x => x.Id == kmv.Id);
            if (khuyenMai != null)
            {
                khuyenMai.Ten = kmv.Ten;
                khuyenMai.GiaTri = kmv.GiaTri;
                khuyenMai.NgayApDung = kmv.NgayApDung;
                khuyenMai.NgayKetThuc = kmv.NgayKetThuc;
                if (khuyenMai.NgayApDung > khuyenMai.NgayKetThuc
                    )
                {
                    return false;
                }
                khuyenMai.MoTa = kmv.MoTa;
                khuyenMai.TrangThai = kmv.TrangThai;
                return khuyenmais.Update(khuyenMai);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<KhuyenMaiController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var khuyenMai = khuyenmais.GetAll().FirstOrDefault(x => x.Id == id);
            if (khuyenMai != null)
            {
               
                
                return khuyenmais.Delete(khuyenMai);
            }
            else
            {
                return false;
            }
        }
    }
}
