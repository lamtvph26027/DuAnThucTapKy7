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
    public class ChiTietPTTTConfiguration : IEntityTypeConfiguration<ChiTietPhuongThucThanhToan>
    {
        public void Configure(EntityTypeBuilder<ChiTietPhuongThucThanhToan> builder)
        {
            builder.ToTable("ChiTietPTTT");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TrangThai).HasColumnType("int").IsRequired();
            builder.Property(x => x.SoTien).HasColumnType("int").IsRequired();
            builder.HasOne(x => x.HoaDons).WithMany(x => x.ChiTietPTTTs).HasForeignKey(x => x.IdHoaDon);
            builder.HasOne(x => x.PhuongThucThanhToans).WithMany(x => x.ChiTietPTTTs).HasForeignKey(x => x.IdPTTT);
        }
    }
}
