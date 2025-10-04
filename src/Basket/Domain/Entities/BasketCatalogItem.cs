namespace Basket.Domain.Entities;

public sealed class BasketCatalogItem
{
    public const string TblName = "BasketCatalogItems";


    public Guid Id { get; set; }

    public string Slug { get; set; }

    public string CatalogItemName { get; set; }

    public decimal Price { get; set; }

    public decimal? LatestPrice { get; set; }

    public bool UserChangedSeen { get; set; }

    public int Quantity { get; set; }


    public bool PriceChanged => LatestPrice.HasValue &&
                                LatestPrice.Value != Price;

    public UserBasket UserBasket { get; internal set; }
    public int UserBasketId { get; internal set; }
}

public sealed class BasketCatalogItemConfiguration : IEntityTypeConfiguration<BasketCatalogItem>
{
    public void Configure(EntityTypeBuilder<BasketCatalogItem> builder)
    {
        builder.ToTable(BasketCatalogItem.TblName);

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Quantity).IsRequired();

        builder.Property(d => d.Slug)
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

        builder.Property(d => d.CatalogItemName)
                                   .IsRequired()
                                   .HasMaxLength(200)
                                   .IsUnicode(true);

        builder.Property(d => d.LatestPrice)
                     .IsRequired(false);


        builder.Property(d => d.UserBasketId)
                     .IsRequired(true);


        builder.Property(d => d.Price)
                    .IsRequired();


        builder.Property(d => d.UserChangedSeen)
                    .IsRequired();
    }
}