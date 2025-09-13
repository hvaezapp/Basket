using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Domain.Entities;

public abstract class UserBasket
{
    public const string TblName = "UserBaskets";

    public int Id { get; set; }

    public int UserId { get; set; }

    public ICollection<BasketCatalogItem> Items { get; set; }

    public abstract void AddItem(BasketCatalogItem primaryItem);

}

public sealed class UserBasketConfiguration : IEntityTypeConfiguration<UserBasket>
{
    public void Configure(EntityTypeBuilder<UserBasket> builder)
    {
        builder.ToTable(UserBasket.TblName);


        builder.HasKey(d => d.Id);


        builder.Property(x => x.UserId).IsRequired();


        builder.HasDiscriminator<string>("BasketType")
                                         .HasValue<PrimaryUserBasket>("Primary")
                                         .HasValue<SecondaryUserBasket>("Secondary");

        builder.HasMany(d => d.Items)
                    .WithOne(f => f.UserBasket)
                    .HasForeignKey(f => f.UserBasketId)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}
