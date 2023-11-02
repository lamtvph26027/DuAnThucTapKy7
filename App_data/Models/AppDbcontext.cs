
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Models
{
   public class AppDbcontext:DbContext
    {
public AppDbcontext()
        {

        }
        public AppDbcontext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }
       public DbSet<Anh> Anhs { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
       
        public DbSet<MauSac> MauSacs { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
       
        public DbSet<LoaiSP> LoaiSPs { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<ChatLieu> ChatLieus { get; set; }
        public DbSet<ChiTietPhuongThucThanhToan> ChiTietPTTTs { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<TrangThaiHoaDon> TrangThaiHoaDons { get; set; }
        public DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-S6G7NFV\SQLEXPRESS;Initial Catalog=Du_An_ThucTapKy_7_Lan2;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
