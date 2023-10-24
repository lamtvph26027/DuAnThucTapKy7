using App_data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace App_data.Configurations
{
    public class HoaDonConfiguration : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.ToTable("HoaDon");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NgayTao).HasColumnType("datetime");
            builder.Property(x => x.NgayThanhToan).HasColumnType("datetime");
            builder.Property(x => x.TenNguoiNhan).HasColumnType("nvarchar(100)");
            builder.Property(x => x.SDT).HasColumnType("nvarchar(10)");
            builder.Property(x => x.Email).HasColumnType("nvarchar(50)");
            builder.Property(x => x.DiaChi).HasColumnType("nvarchar(100)");
            builder.Property(x => x.TienShip).HasColumnType("int");
            builder.Property(x => x.PhuongThucThanhToan).HasColumnType("nvarchar(20)");
           
            builder.HasOne(x => x.TaiKhoans).WithMany(x => x.HoaDons).HasForeignKey(x => x.IdNguoiDung);
            builder.HasOne(x => x.Trangthais).WithMany(x => x.HoaDons).HasForeignKey(x => x.IdTrangThaiGiaoHang);

        }
    }
}
