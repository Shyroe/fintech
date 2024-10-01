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
    public class NotaFiscalMapping : IEntityTypeConfiguration<NotaFiscal>
    {
        public void Configure(EntityTypeBuilder<NotaFiscal> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()")
            .ValueGeneratedOnAdd();

            builder.Property(p => p.NomePagador)
            .IsRequired()
            .HasMaxLength(150);

            builder.Property(p => p.NumeroIdentificacao)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.DataEmissao)
                .IsRequired();

            builder.Property(p => p.DataCobranca)
                .IsRequired(false);

            builder.Property(p => p.DataPagamento)
                .IsRequired(false);

            builder.Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DocumentoNotaFiscal)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(p => p.DocumentoBoletoBancario)
                .IsRequired(false)
                .HasMaxLength(200);

            // Chave estrangeira para StatusNotaFiscal
            builder.HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict); // Define a restrição de exclusão

            // Configurações de timestamp
            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            builder.ToTable("NotasFiscais");
        }
    }
}
