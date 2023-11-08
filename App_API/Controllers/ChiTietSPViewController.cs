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
                                         idSanPham=CTSP.IdSanPham,
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
                                     idSanPham=CTSP.IdSanPham,
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
            idSanPham=c.CTSP.IdSanPham,
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
                                     idSanPham=CTSP.IdSanPham,
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
        [Route("laytatcattt")]
        [HttpGet]
        public async Task<ChiTietSanPham> GetByIdtt(Guid id)
        {
            var sanPham = await _dbcontext.ChiTietSanPhams
                .Include(sp => sp.listAnhs).Include(sp=>sp.listMauSacs).Include(sp=>sp.listSizes)
                .FirstOrDefaultAsync(sp => sp.Id == id);

            return sanPham;

            
        }

        // POST api/<ChiTietSPViewController>

        [Route("UpdateNhieuThuocTinh")]
        [HttpPut]
        public bool UpdateThuocTinh(ChiTietSPModel request)
        {
            try
            {
                // lay chi tiet san pham tu co so du lieu
                var ct =  _dbcontext.ChiTietSanPhams.Include(sp => sp.listAnhs).Include(sp => sp.listMauSacs).Include(sp => sp.listSizes).FirstOrDefault(sp => sp.Id == request.id);

                if (ct == null)
                {
                    return false;
                }
                else
                {

                    // Xóa các ảnh cũ
                    foreach (var anh in ct.listAnhs.ToList())
                    {
                        if (!request.listAnhs.Contains(anh.Id))
                        {
                            ct.listAnhs.Remove(anh);
                        }
                    }
                    ct.listAnhs = _dbcontext.Anhs.Where(a => request.listAnhs.Contains(a.Id)).ToList();
                    //// Thêm các ảnh mới
                    //foreach (var idAnh in request.listAnhs)
                    //{
                    //    if (!ct.listAnhs.Any(anh => anh.Id == idAnh))
                    //    {
                    //        var anh = new Anh { Id = idAnh };
                    //        ct.listAnhs.Add(anh);
                    //    }
                    //}
                    // Xóa các mausac cũ
                    foreach (var anh in ct.listMauSacs.ToList())
                    {
                        if (!request.listAnhs.Contains(anh.Id))
                        {
                            ct.listMauSacs.Remove(anh);
                        }
                    }
                    ct.listMauSacs = _dbcontext.MauSacs.Where(a => request.listMauSacs.Contains(a.Id)).ToList();
                    //// Thêm các mausac mới
                    //foreach (var idAnh in request.listMauSacs)
                    //{
                    //    if (!ct.listMauSacs.Any(anh => anh.Id == idAnh))
                    //    {
                    //        var anh = new MauSac { Id = idAnh };
                    //        ct.listMauSacs.Add(anh);
                    //    }
                    //}
                    // Xóa các size cũ
                    foreach (var anh in ct.listSizes.ToList())
                    {
                        if (!request.listSizes.Contains(anh.Id))
                        {
                            ct.listSizes.Remove(anh);
                        }
                    }

                    //// Thêm các size mới
                    //foreach (var idAnh in request.listSizes)
                    //{
                    //    if (!ct.listSizes.Any(anh => anh.Id == idAnh))
                    //    {
                    //        var anh = new Size { Id = idAnh };
                    //        ct.listSizes.Add(anh);
                    //    }
                    //}
                    ct.listSizes = _dbcontext.Sizes.Where(a => request.listSizes.Contains(a.Id)).ToList();


                    ct.soluong = request.soluong;
                    ct.DonGia = request.DonGia;
                    ct.TrangThai = request.TrangThai;
                    _dbcontext.ChiTietSanPhams.Update(ct);
                    _dbcontext.SaveChanges();

                    return true;
                    
                }
            }
            catch
            {
                return false;
            }

        }


        // PUT api/<ChiTietSPViewController>/5
        [Route("QLKhuyenMai")]
        [HttpPut]
        public bool AddKMVoSP(List<Guid> qlkm,Guid IdKhuyenMai)
        {
            foreach(var km in qlkm)
            {
                var timidkm=_dbcontext.KhuyenMais.FirstOrDefault(x => x.Id == IdKhuyenMai);
                if (timidkm.NgayKetThuc < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    var tim = _dbcontext.ChiTietSanPhams.FirstOrDefault(x => x.Id == km);
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
        [Route("QLKhuyenMaiDelete")]
        [HttpPut]
        public bool DeleteKMVoSP(List<Guid> qlkm)
        {
            foreach (var km in qlkm)
            { 
                    var tim = _dbcontext.ChiTietSanPhams.FirstOrDefault(x => x.Id == km);
                    if (tim != null)
                    {
                        tim.IdKhuyenMai = null;
                        _dbcontext.ChiTietSanPhams.Update(tim);
                        _dbcontext.SaveChanges();
                    }
            }
            return true;
        }

        [Route("ALlSPKhuyenMai")]
        [HttpGet]
        public async Task<List<AllSanPhamByKM>> GetAllSPKhuyenMai()
        {
            var AllCTSP = await (from CTSP in _dbcontext.ChiTietSanPhams.AsNoTracking()

                                 join sp in _dbcontext.SanPhams.AsNoTracking() on CTSP.IdSanPham equals sp.Id

                                 join anh in _dbcontext.Anhs.AsNoTracking() on CTSP.IdAnh equals anh.Id
                                 join loaisp in _dbcontext.LoaiSPs.AsNoTracking() on CTSP.IdLoaiSP equals loaisp.Id

                                 select new AllSanPhamByKM()
                                 {
                                     Id = sp.Id,
                                     TenAnh = anh.Ten,
                                     IdLoaiSP = loaisp.Id,
                                     IdLoaiSpCha=loaisp.IdLoaiSPCha,
                                     Ten = sp.Ten,
                                     MoTa = sp.MoTa,
                                     IdKhuyenMai = (from km in _dbcontext.KhuyenMais where CTSP.IdKhuyenMai == km.Id select km.Id).FirstOrDefault(),
                                     TrangThai = sp.TrangThai
                                 }).ToListAsync();
            return AllCTSP;
        }
        [Route("ALlSPByLoaiSP")]
        [HttpGet]
        public async Task<List<AllSanPhamByKM>> GetAllSPByLoaiSP(Guid id,Guid? IdLoaiSPCha)
        {
            var AllCTSP = await (from CTSP in _dbcontext.ChiTietSanPhams.AsNoTracking()

                                 join sp in _dbcontext.SanPhams.AsNoTracking() on CTSP.IdSanPham equals sp.Id

                                 join anh in _dbcontext.Anhs.AsNoTracking() on CTSP.IdAnh equals anh.Id
                                 join loaisp in _dbcontext.LoaiSPs.AsNoTracking() on CTSP.IdLoaiSP equals loaisp.Id
                                 

                                 select new AllSanPhamByKM()
                                 {
                                     Id = sp.Id,
                                     TenAnh = anh.Ten,
                                     IdLoaiSP = loaisp.Id,
                                     IdLoaiSpCha = loaisp.IdLoaiSPCha,
                                     Ten = sp.Ten,
                                     MoTa = sp.MoTa,
                                     IdKhuyenMai = (from km in _dbcontext.KhuyenMais where CTSP.IdKhuyenMai == km.Id select km.Id).FirstOrDefault(),
                                     TrangThai = sp.TrangThai
                                 }).Where(x=>x.IdLoaiSP==id||x.IdLoaiSP==id&&x.IdLoaiSpCha==IdLoaiSPCha).ToListAsync();
            return AllCTSP;
        }
        // DELETE api/<ChiTietSPViewController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
