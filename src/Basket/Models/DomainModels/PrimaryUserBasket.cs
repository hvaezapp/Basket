namespace Basket.Models.DomainModels;

public class PrimaryUserBasket : UserBasket
{

    public void AddItem(string slug, decimal price, string catalogItemName)
    {
        Items ??= [];

        var item = Items.FirstOrDefault(d => d.Slug == slug);

        if (item is null)
        {
            Items.Add(new BasketCatalogItem
            {
                Slug = slug,
                Price = price,
                Quantity = 1,
                CatalogItemName = catalogItemName
            });
        }
        else
        {
            item.Quantity++;
        }

    }

    public override void AddItem(BasketCatalogItem item)
    {
        AddItem(item.Slug, item.Price, item.CatalogItemName);
    }

    internal void CleanUp()
    {
        Items.Clear();
    }

    internal void DecreaseQuantity(Guid id)
    {
        var item = Items.First(d => d.Id == id);
        item.Quantity--;

        if (item.Quantity == 0)
            Items.Remove(item);
    }

    internal void IncreaseQuantity(Guid id)
    {
        var item = Items.First(d => d.Id == id);
        item.Quantity++;
    }

    internal void Remove(Guid itemId)
    {
        var item = Items.First(d => d.Id == itemId);
        Items.Remove(item);
    }
}
