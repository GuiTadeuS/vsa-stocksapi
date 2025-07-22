using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StocksApi.Models;

namespace StocksApi.Data.Mappings;
public class StockMapping : IEntityTypeConfiguration<Stock>
{
       public void Configure(EntityTypeBuilder<Stock> builder)
       {
              builder.ToTable("stocks");

              builder.HasKey(s => s.Id);

              builder.Property(s => s.Symbol)
                     .IsRequired()
                     .HasMaxLength(10);

              builder.Property(s => s.CompanyName)
                     .IsRequired()
                     .HasMaxLength(255);

              builder.Property(s => s.Purchase)
                     .HasColumnType("decimal(18,2)");

              builder.Property(s => s.LastDiv)
                     .HasColumnType("decimal(18,2)");

              builder.Property(s => s.Industry)
                     .HasMaxLength(100);

              builder.Property(s => s.MarketCap)
                     .IsRequired();

              builder.HasMany(s => s.Comments)
                     .WithOne(c => c.Stock)
                     .HasForeignKey(c => c.StockId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasMany(s => s.Portfolios)
                     .WithOne(p => p.Stock);
       }
}