namespace Basket.Models.DomainModels;

public class BasketCatalogItem
{
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
