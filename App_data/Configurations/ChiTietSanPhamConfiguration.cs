using App_data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Configurations
{
    public class ChiTietSanPhamConfiguration : IEntityTypeConfiguration<ChiTietSanPham>
    {
        public void Configure(EntityTypeBuilder<ChiTietSanPham> builder)
        {
            builder.ToTable("ChiTietSanPham");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TrangThai).HasColumnType("int").IsRequired();
            builder.HasOne(x => x.MauSacs).WithMany(x => x.ChiTietSanPhams).HasForeignKey(x => x.IdMauSac);
            builder.HasOne(x => x.NhaCungCaps).WithMany(x => x.ChiTietSanPhams).HasForeignKey(x => x.IdNhaCungCap);
            builder.HasOne(x => x.LoaiSPs).WithMany(x => x.ChiTietSanPhams).HasForeignKey(x => x.IdLoaiSP);
            builder.HasOne(x => x.Sizes).WithMany(x => x.ChiTietSanPhams).HasForeignKey(x => x.IdSize);
            builder.HasOne(x => x.Anhs).WithMany(x => x.ChiTietSanPhams).HasForeignKey(x => x.IdAnh);
            builder.HasOne(x => x.SanPhams).WithMany(x => x.ChiTietSanPhams).HasForeignKey(x => x.IdSanPham);
            builder.HasOne(x => x.KhuyenMais).WithMany(x => x.ChiTietSanPhams).HasForeignKey(x => x.IdKhuyenMai);

        }
    }
}
