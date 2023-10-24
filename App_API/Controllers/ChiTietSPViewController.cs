using App_data.Models;
using App_data.ViewModels;
using App_data.ViewModels.ChiTietSPView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietSPViewController : ControllerBase
    {
        private readonly AppDbcontext _dbcontext;
        public ChiTietSPViewController()
        {
            _dbcontext = new AppDbcontext();
        }
        // GET: api/<ChiTietSPViewController>
       
        [HttpGet]
        public async Task<List<AllChiTietSanPham>> GetAllCTSPSanPham()
        {
           
            
                var AllCTSP = await (from CTSP in _dbcontext.ChiTietSanPhams.AsNoTracking()
                                     join anh in _dbcontext.Anhs.AsNoTracking() on CTSP.IdAnh equals anh.Id
                                     join mausac in _dbcontext.MauSacs.AsNoTracking() on CTSP.IdMauSac equals mausac.Id
                                     join size in _dbcontext.Sizes.AsNoTracking() on CTSP.IdSize equals size.Id
                                     join nhacungcap in _dbcontext.NhaCungCaps.AsNoTracking() on CTSP.IdNhaCungCap equals nhacungcap.Id
                                     join loaisp in _dbcontext.LoaiSPs.AsNoTracking() on CTSP.IdLoaiSP equals loaisp.Id
                                     join sp in _dbcontext.SanPhams.AsNoTracking() on CTSP.IdSanPham equals sp.Id
                                    
                                     select new AllChiTietSanPham()
                                     {

                                         Id = CTSP.Id,
                                         TenAnh = anh.Ten,
                                         MaKhuyenMai= null,
                                         TenLoaiSP = loaisp.Ten,
                                         TenSanPham = sp.Ten,
                                         TenMauSac = mausac.Ten,
                                         TenSize = size.Ten,
                                         TenNhaCungCap = nhacungcap.Ten,
                                         GiaGoc = CTSP.DonGia,
                                         GiaKhuyenMai = CTSP.DonGia,
                                         soluong = CTSP.soluong,
                                         TrangThai = CTSP.TrangThai



                                     }).ToListAsync();
                return AllCTSP;
            
            
        }
        [Route("TimKiem")]
        [HttpGet]
        public async Task<List<AllChiTietSanPham>> SearchAllCTSPSanPham(string name)
        {


            var AllCTSP = await (from CTSP in _dbcontext.ChiTietSanPhams.AsNoTracking()
                                 join anh in _dbcontext.Anhs.AsNoTracking() on CTSP.IdAnh equals anh.Id
                                 join mausac in _dbcontext.MauSacs.AsNoTracking() on CTSP.IdMauSac equals mausac.Id
                                 join size in _dbcontext.Sizes.AsNoTracking() on CTSP.IdSize equals size.Id
                                 join nhacungcap in _dbcontext.NhaCungCaps.AsNoTracking() on CTSP.IdNhaCungCap equals nhacungcap.Id
                                 join loaisp in _dbcontext.LoaiSPs.AsNoTracking() on CTSP.IdLoaiSP equals loaisp.Id
                                 join sp in _dbcontext.SanPhams.AsNoTracking() on CTSP.IdSanPham equals sp.Id

                                 select new AllChiTietSanPham()
                                 {

                                     Id = CTSP.Id,
                                     TenAnh = anh.Ten,
                                     MaKhuyenMai = null,
                                     TenLoaiSP = loaisp.Ten,
                                     TenSanPham = sp.Ten,
                                     TenMauSac = mausac.Ten,
                                     TenSize = size.Ten,
                                     TenNhaCungCap = nhacungcap.Ten,
                                     GiaGoc = CTSP.DonGia,
                                     GiaKhuyenMai = CTSP.DonGia,
                                     soluong = CTSP.soluong,
                                     TrangThai = CTSP.TrangThai



                                 }).Where(x=>x.TenSanPham.ToLower().Contains(name.ToLower())).ToListAsync();
            return AllCTSP;


        }
        [Route("TimKiemNangCao")]
        [HttpPost]
        public async Task<List<AllChiTietSanPham>> SearchNangCaoCTSPSanPham( TimKiemSanPhamNangCao tknc)
        {


            var AllCTSP =  (from CTSP in _dbcontext.ChiTietSanPhams.AsNoTracking()
                                 join anh in _dbcontext.Anhs.AsNoTracking() on CTSP.IdAnh equals anh.Id
                                 join mausac in _dbcontext.MauSacs.AsNoTracking() on CTSP.IdMauSac equals mausac.Id
                                 join size in _dbcontext.Sizes.AsNoTracking() on CTSP.IdSize equals size.Id
                                 join nhacungcap in _dbcontext.NhaCungCaps.AsNoTracking() on CTSP.IdNhaCungCap equals nhacungcap.Id
                                 join loaisp in _dbcontext.LoaiSPs.AsNoTracking() on CTSP.IdLoaiSP equals loaisp.Id
                                 join sp in _dbcontext.SanPhams.AsNoTracking() on CTSP.IdSanPham equals sp.Id
                                 select new { CTSP,anh,mausac,size,nhacungcap,loaisp,sp});
            //tim Gia theo gia min,gia max
            if (tknc.minGia != 0 && tknc.maxGia != 0)
            {
                AllCTSP=AllCTSP.AsNoTracking().Where(c=>c.CTSP.DonGia>=tknc.minGia&&c.CTSP.DonGia<=tknc.maxGia);
            }
             if (!string.IsNullOrEmpty(tknc.TenMauSac))
            {
                AllCTSP=AllCTSP.AsNoTracking().Where(c=>c.mausac.Ten.Contains(tknc.TenMauSac));
            }
            if (!string.IsNullOrEmpty(tknc.TenSize))
            {
                AllCTSP = AllCTSP.AsNoTracking().Where(c => c.size.Ten.Contains(tknc.TenSize));
            }
            var result = await AllCTSP.AsNoTracking().Select(c => new AllChiTietSanPham() {  
            Id=c.CTSP.Id,
            TenAnh=c.anh.Ten,
            MaKhuyenMai=null,
            TenSanPham=c.sp.Ten,
            TenLoaiSP=c.loaisp.Ten,
            TenMauSac=c.mausac.Ten,
            TenSize=c.size.Ten,
            GiaGoc=c.CTSP.DonGia,
            GiaKhuyenMai=c.CTSP.DonGia,
            soluong=c.CTSP.soluong,
            TrangThai=c.CTSP.TrangThai,

            }).ToListAsync();
            return result;


        }

        // GET api/<ChiTietSPViewController>/5
        [Route("idSanPham")]
        [HttpGet]
        public async Task<List<AllChiTietSanPham>> GetCTSPByIdSanPham(Guid id)
        {
            if (!_dbcontext.SanPhams.Any(c => c.Id == id)) throw new Exception($" khong tim thay san pham co id:{id}");

            var AllCTSP = await (from CTSP in _dbcontext.ChiTietSanPhams.AsNoTracking()
                                 join anh in _dbcontext.Anhs.AsNoTracking() on CTSP.IdAnh equals anh.Id
                                 join mausac in _dbcontext.MauSacs.AsNoTracking() on CTSP.IdMauSac equals mausac.Id
                                 join size in _dbcontext.Sizes.AsNoTracking() on CTSP.IdSize equals size.Id
                                 join nhacungcap in _dbcontext.NhaCungCaps.AsNoTracking() on CTSP.IdNhaCungCap equals nhacungcap.Id
                                 join loaisp in _dbcontext.LoaiSPs.AsNoTracking() on CTSP.IdLoaiSP equals loaisp.Id
                                 join sp in _dbcontext.SanPhams.AsNoTracking() on CTSP.IdSanPham equals sp.Id
                                 where CTSP.IdSanPham == id

                                 select new AllChiTietSanPham()
                                 {
                                     Id = CTSP.Id,
                                     TenAnh = anh.Ten,
                                     MaKhuyenMai=null,
                                     TenLoaiSP = loaisp.Ten,
                                     TenSanPham = sp.Ten,
                                     TenMauSac = mausac.Ten,
                                     TenSize = size.Ten,
                                     TenNhaCungCap = nhacungcap.Ten,
                                     GiaGoc=CTSP.DonGia,
                                     GiaKhuyenMai = CTSP.DonGia,
                                     soluong = CTSP.soluong,
                                     TrangThai = CTSP.TrangThai
                                    


                                 }).ToListAsync();
            return AllCTSP;


        }
        // GET api/<ChiTietSPViewController>/5
        [HttpGet("{id}")]
        public async Task<AllChiTietSanPham> GetCTSPById(Guid id)
        {
            if (!_dbcontext.ChiTietSanPhams.Any(c => c.Id == id)) throw new Exception($" khong tim thay san pham co id:{id}");

            var AllCTSP = await (from CTSP in _dbcontext.ChiTietSanPhams.AsNoTracking()
                                 join anh in _dbcontext.Anhs.AsNoTracking() on CTSP.IdAnh equals anh.Id
                                 join mausac in _dbcontext.MauSacs.AsNoTracking() on CTSP.IdMauSac equals mausac.Id
                                 join size in _dbcontext.Sizes.AsNoTracking() on CTSP.IdSize equals size.Id
                                 join nhacungcap in _dbcontext.NhaCungCaps.AsNoTracking() on CTSP.IdNhaCungCap equals nhacungcap.Id
                                 join loaisp in _dbcontext.LoaiSPs.AsNoTracking() on CTSP.IdLoaiSP equals loaisp.Id
                                 join sp in _dbcontext.SanPhams.AsNoTracking() on CTSP.IdSanPham equals sp.Id
                                 where CTSP.Id == id

                                 select new AllChiTietSanPham()
                                 {
                                     Id = CTSP.Id,
                                     TenAnh = anh.Ten,
                                     MaKhuyenMai = null,
                                     TenLoaiSP = loaisp.Ten,
                                     TenSanPham = sp.Ten,
                                     TenMauSac = mausac.Ten,
                                     TenSize = size.Ten,
                                     TenNhaCungCap = nhacungcap.Ten,
                                     GiaGoc = CTSP.DonGia,
                                     GiaKhuyenMai = CTSP.DonGia,
                                     soluong = CTSP.soluong,
                                     TrangThai = CTSP.TrangThai



                                 }).FirstOrDefaultAsync();
            return AllCTSP;


        }

        // POST api/<ChiTietSPViewController>
        [HttpPost]
        public bool Post(ChiTietSPModel chiTietSPModel)
        {
            ChiTietSanPham chiTietSanPham = new ChiTietSanPham();
            chiTietSanPham.Id = Guid.NewGuid();
            chiTietSanPham.IdKhuyenMai = chiTietSPModel.IdKhuyenMai;
            chiTietSanPham.IdAnh=chiTietSPModel.IdAnh;
            chiTietSanPham.IdMauSac=chiTietSPModel.IdMauSac;
            chiTietSanPham.IdSanPham = chiTietSPModel.IdSanPham;
            chiTietSanPham.IdNhaCungCap = chiTietSPModel.IdNhaCungCap;
            chiTietSanPham.IdSize = chiTietSPModel.IdSize;
            chiTietSanPham.IdLoaiSP = chiTietSPModel.IdLoaiSP;
            chiTietSanPham.DonGia=chiTietSPModel.DonGia;
            chiTietSanPham.soluong = chiTietSanPham.soluong;
            chiTietSanPham.TrangThai = chiTietSPModel.TrangThai;
            _dbcontext.ChiTietSanPhams.Add(chiTietSanPham);
            _dbcontext.SaveChanges();
            foreach(var litsanhs in chiTietSPModel.listAnhs)
            {
                var timanh = _dbcontext.Anhs.FirstOrDefault(x => x.Id == chiTietSanPham.IdAnh);
                if(timanh==null)
               
                timanh.Id=Guid.NewGuid();
                timanh.Ten=litsanhs.Ten;
                timanh.TrangThai = 1;
                _dbcontext.Anhs.Add(timanh);
                _dbcontext.SaveChanges();
            }
            foreach (var litsmausacs in chiTietSPModel.listMauSacs)
            {
                var timmausac = _dbcontext.MauSacs.FirstOrDefault(x => x.Id == chiTietSanPham.IdMauSac);
                if (timmausac == null)

                    timmausac.Id = Guid.NewGuid();
                timmausac.Ten = litsmausacs.Ten;
                timmausac.AnhMauSac = litsmausacs.AnhMauSac;
                timmausac.TrangThai = 1;
                _dbcontext.MauSacs.Add(timmausac);
                _dbcontext.SaveChanges();
            }
            foreach (var litsizes in chiTietSPModel.listSizes)
            {
                var timsize = _dbcontext.Sizes.FirstOrDefault(x => x.Id == chiTietSanPham.IdSize);
                if (timsize == null)

                    timsize.Id = Guid.NewGuid();
                timsize.Ten = litsizes.Ten;
                timsize.TrangThai = 1;
                _dbcontext.Sizes.Add(timsize);
                _dbcontext.SaveChanges();
            }
            return true;

        }
       

        // PUT api/<ChiTietSPViewController>/5
        [Route("QLKhuyenMai")]
        [HttpPut]
        public bool AddKMVoSP(List<ChiTietSanPham> qlkm,Guid IdKhuyenMai)
        {
            foreach(var km in qlkm)
            {
                var timidkm=_dbcontext.KhuyenMais.FirstOrDefault(x => x.Id == km.IdKhuyenMai);
                if (timidkm.NgayKetThuc < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    var tim = _dbcontext.ChiTietSanPhams.FirstOrDefault(x => x.Id == km.Id);
                    if (tim != null)
                    {
                        tim.IdKhuyenMai = IdKhuyenMai;
                        _dbcontext.ChiTietSanPhams.Update(tim);
                        _dbcontext.SaveChanges();

                    }
                }
                
            }
            return true;
        }

        // DELETE api/<ChiTietSPViewController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
