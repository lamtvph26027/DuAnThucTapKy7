using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using App_data.ViewModels;
using App_data.ViewModels.ChiTietSPView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuyenMaiController : ControllerBase
    {
        private readonly IAllRepositories<KhuyenMai> khuyenmais;
        private readonly AppDbcontext context;
        public KhuyenMaiController()
        {   context = new AppDbcontext();
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
        [Route("GetAllSPByKm")]
        [HttpGet]
        public List<AllSanPhamByKM> GetAllSPByKm(Guid id)
        {
            var result = context.ChiTietSanPhams
                .Join(context.SanPhams, ctsp_sp => ctsp_sp.IdSanPham, sp => sp.Id, (ctsp_sp, sp) => new { ChiTietSanPham = ctsp_sp, SanPham = sp })
                .Join(context.LoaiSPs, ctsp_lsp => ctsp_lsp.ChiTietSanPham.IdLoaiSP, lsp => lsp.Id, (ctsp_sp, lsp) => new { ChiTietSanPham_LoaiSP = ctsp_sp, LoaiSP = lsp })
                .GroupBy(x => x.ChiTietSanPham_LoaiSP.SanPham.Id)
                .Select(x => new AllSanPhamByKM {
                    Id=x.FirstOrDefault().ChiTietSanPham_LoaiSP.SanPham.Id,
                    IdLoaiSP=x.FirstOrDefault().LoaiSP.Id,
                    IdLoaiSpCha=x.FirstOrDefault().LoaiSP.IdLoaiSPCha,
                    Ten=x.FirstOrDefault().ChiTietSanPham_LoaiSP.SanPham.Ten,
                    SLCTSP=x.Sum(y=>y.ChiTietSanPham_LoaiSP.ChiTietSanPham.Id!=null?1:0),
                    MoTa= x.FirstOrDefault().ChiTietSanPham_LoaiSP.SanPham.MoTa,
                    TrangThai= x.FirstOrDefault().ChiTietSanPham_LoaiSP.SanPham.TrangThai,
                    IdKhuyenMai= (from km in context.KhuyenMais where x.FirstOrDefault().ChiTietSanPham_LoaiSP.ChiTietSanPham.IdKhuyenMai == km.Id select km.Id).FirstOrDefault(),
                    TenAnh= (from anh in context.Anhs where x.FirstOrDefault().ChiTietSanPham_LoaiSP.ChiTietSanPham.IdAnh == anh.Id select anh.Ten).FirstOrDefault()
                }).Where(x=>x.IdKhuyenMai==id).ToList();
            return result;

        }
        [Route("GetAllSPNoKm")]
        [HttpGet]
        public List<AllSanPhamByKM> GetAllSPNoKm(Guid id)
        {
            var result = context.ChiTietSanPhams
                .Join(context.SanPhams, ctsp_sp => ctsp_sp.IdSanPham, sp => sp.Id, (ctsp_sp, sp) => new { ChiTietSanPham = ctsp_sp, SanPham = sp })
                .Join(context.LoaiSPs, ctsp_lsp => ctsp_lsp.ChiTietSanPham.IdLoaiSP, lsp => lsp.Id, (ctsp_sp, lsp) => new { ChiTietSanPham_LoaiSP = ctsp_sp, LoaiSP = lsp })
                .GroupBy(x => x.ChiTietSanPham_LoaiSP.SanPham.Id)
                .Select(x => new AllSanPhamByKM
                {
                    Id = x.FirstOrDefault().ChiTietSanPham_LoaiSP.SanPham.Id,
                    IdLoaiSP = x.FirstOrDefault().LoaiSP.Id,
                    IdLoaiSpCha = x.FirstOrDefault().LoaiSP.IdLoaiSPCha,
                    Ten = x.FirstOrDefault().ChiTietSanPham_LoaiSP.SanPham.Ten,
                    SLCTSP = x.Sum(y => y.ChiTietSanPham_LoaiSP.ChiTietSanPham.Id != null ? 1 : 0),
                    MoTa = x.FirstOrDefault().ChiTietSanPham_LoaiSP.SanPham.MoTa,
                    TrangThai = x.FirstOrDefault().ChiTietSanPham_LoaiSP.SanPham.TrangThai,
                    IdKhuyenMai = (from km in context.KhuyenMais where x.FirstOrDefault().ChiTietSanPham_LoaiSP.ChiTietSanPham.IdKhuyenMai == km.Id select km.Id).FirstOrDefault(),
                    TenAnh = (from anh in context.Anhs where x.FirstOrDefault().ChiTietSanPham_LoaiSP.ChiTietSanPham.IdAnh == anh.Id select anh.Ten).FirstOrDefault()
                }).Where(x => x.IdKhuyenMai != id).ToList();
            return result;

        }
        

    }
}
