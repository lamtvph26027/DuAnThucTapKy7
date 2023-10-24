using App_data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace App_data.Configurations
{
    public class TaiKhoanConfiguration : IEntityTypeConfiguration<TaiKhoan>
    {
        public void Configure(EntityTypeBuilder<TaiKhoan> builder)
        {
            builder.ToTable("TaiKhoan");
            builder.HasKey(x => x.IdNguoiDung);
            builder.Property(x => x.Ten).HasColumnType("nvarchar(15)");
            builder.Property(x => x.TenDem).HasColumnType("nvarchar(15)");
            builder.Property(x => x.Ho).HasColumnType("nvarchar(15)");
            builder.Property(x => x.Password).HasColumnType("varchar(15)");
            builder.Property(x => x.GioiTinh).HasColumnType("int");
            builder.Property(x => x.NgaySinh).HasColumnType("datetime");
            builder.Property(x => x.Email).HasColumnType("varchar(50)");
            builder.Property(x => x.DiaChi).HasColumnType("nvarchar(100)");
            builder.Property(x => x.SDT).HasColumnType("varchar(10)");
           
            builder.HasOne(x => x.VaiTros).WithMany(x => x.TaiKhoans).HasForeignKey(x => x.IdVaiTro);
            builder.HasOne(x => x.GioHang).WithOne(x => x.TaiKhoan).HasForeignKey<GioHang>();
        }
    }
}
