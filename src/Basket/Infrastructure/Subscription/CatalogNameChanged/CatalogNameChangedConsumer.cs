using Basket.Infrastructure.Persistence.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Basket.Infrastructure.Subscription.CatalogNameChanged;

public class CatalogNameChangedConsumer(BasketDbContext dbContext) : IConsumer<CatalogNameChangedEvent>
{
    private readonly BasketDbContext _dbContext = dbContext;

    public async Task Consume(ConsumeContext<CatalogNameChangedEvent> context)
    {
        await _dbContext.BasketCatalogItems
                   .Where(x => x.Slug == context.Message.Slug)
                   .ExecuteUpdateAsync(d => d.SetProperty(d => d.CatalogItemName,
                   context.Message.Name));
    }
}