using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StocksApi.Models;

namespace StocksApi.Data.Mappings;
public class CommentMapping : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Title)
               .IsRequired()
               .HasMaxLength(100);
        
        builder.Property(c => c.Content)
               .IsRequired()
               .HasMaxLength(1000);
        
        builder.Property(c => c.CreatedOn)
               .IsRequired();
        
        builder.HasOne(c => c.Stock)
               .WithMany(s => s.Comments)
               .HasForeignKey(c => c.StockId)
               .OnDelete(DeleteBehavior.Cascade); 
    }
}