namespace Basket.Domain.Entities;

public sealed class SecondaryUserBasket : UserBasket
{
    public override void AddItem(BasketCatalogItem primaryItem)
    {
        Items ??= [];

        var item = Items.FirstOrDefault(d => d.Slug == primaryItem.Slug);

        if (item is null)
        {
            Items.Add(new BasketCatalogItem
            {
                Slug = primaryItem.Slug,
                Price = primaryItem.Price,
                Quantity = 1,
                CatalogItemName = primaryItem.CatalogItemName
            });
        }
        else
        {
            item.Quantity++;
        }
    }
}