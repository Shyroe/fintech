using FinTech.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.Data.Mappings
{
    public class StatusNotaFiscalMapping : IEntityTypeConfiguration<StatusNotaFiscal>
    {
        public void Configure(EntityTypeBuilder<StatusNotaFiscal> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(50)");            

            builder.ToTable("StatusNotaFiscal");
        }
    }
}
