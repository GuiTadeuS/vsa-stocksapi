using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StocksApi.Models;

namespace StocksApi.Data.Mappings;
public class PortfolioMapping : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ToTable("portfolios");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Content)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(p => p.CreatedAt)
               .IsRequired();

        builder.HasOne(p => p.Stock)
               .WithMany(s => s.Portfolios)
               .HasForeignKey(p => p.StockId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
