using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StocksApi.Models;

namespace StocksApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Stock> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
