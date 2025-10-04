using Basket.Infrastructure.Persistence.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;
namespace Basket.Infrastructure.Subscription.PriceChanged;

public class PriceChangedConsumer(BasketDbContext dbContext) : IConsumer<PriceChangedEvent>
{
    private readonly BasketDbContext _dbContext = dbContext;

    public async Task Consume(ConsumeContext<PriceChangedEvent> context)
    {
        await _dbContext.BasketCatalogItems
                        .Where(x => x.Slug == context.Message.Slug)
                        .ExecuteUpdateAsync(d => d.SetProperty(d => d.Price, context.Message.Price)
                            .SetProperty(d => d.LatestPrice, d => d.Price)
                            .SetProperty(f => f.UserChangedSeen, false));
    }
}
