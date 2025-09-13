using Basket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Basket.Infrastructure.Persistence.Context;

public class BasketDbContext(DbContextOptions<BasketDbContext> dbContextOptions)
    : DbContext(dbContextOptions)
{
    private const string DefaultSchema = "basket";
    public const string DefaultConnectionStringName = "SvcDbContext";


    public DbSet<UserBasket> UserBaskets { get; set; }
    public DbSet<PrimaryUserBasket> PrimaryUserBaskets { get; set; }
    public DbSet<SecondaryUserBasket> SecondaryUserBaskets { get; set; }
    public DbSet<BasketCatalogItem> BasketCatalogItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
