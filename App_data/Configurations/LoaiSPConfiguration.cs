using App_data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace App_data.Configurations
{
    public class LoaiSPConfiguration : IEntityTypeConfiguration<LoaiSP>
    {
        public void Configure(EntityTypeBuilder<LoaiSP> builder)
        {
            builder.ToTable("LoaiSP");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Ten).HasColumnType("nvarchar(30)").IsRequired();
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.HasOne(x => x.LoaiSPChas).WithMany().HasForeignKey(x => x.IdLoaiSPCha);
            builder.HasOne(x => x.ChatLieus).WithMany(x => x.LoaiSPs).HasForeignKey(x => x.IdChatLieu);
        }
    }
}
