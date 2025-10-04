namespace Basket.Infrastructure.Subscription.PriceChanged;
public record PriceChangedEvent(string Slug, decimal Price);
