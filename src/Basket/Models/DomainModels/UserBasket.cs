namespace Basket.Models.DomainModels;

public abstract class UserBasket
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public ICollection<BasketCatalogItem> Items { get; set; }

    public abstract void AddItem(BasketCatalogItem primaryItem);

}
