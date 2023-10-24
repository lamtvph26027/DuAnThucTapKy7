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
    public class DanhGiaConfiguration : IEntityTypeConfiguration<DanhGia>
    {
        public void Configure(EntityTypeBuilder<DanhGia> builder)
        {
            builder.ToTable("DanhGia");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TrangThai).HasColumnType("int").IsRequired();
            builder.Property(x => x.Sao).HasColumnType("int");
            builder.Property(x => x.BinhLuan).HasColumnType("nvarchar(100)");
            builder.HasOne(x => x.ChiTietSanPhams).WithMany(x => x.DanhGias).HasForeignKey(x => x.IdChiTietSP);
            builder.HasOne(x => x.TaiKhoans).WithMany(x => x.DanhGias).HasForeignKey(x => x.IdNguoiDung);

        }
    }
}
