﻿using App_data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_data.Configurations
{
    public class PhuongThucThanhToanConfiguration : IEntityTypeConfiguration<PhuongThucThanhToan>
    {
        public void Configure(EntityTypeBuilder<PhuongThucThanhToan> builder)
        {
            builder.ToTable("PhuongThucThanhToan");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TrangThai).HasColumnType("int").IsRequired();
        }
    }
}
