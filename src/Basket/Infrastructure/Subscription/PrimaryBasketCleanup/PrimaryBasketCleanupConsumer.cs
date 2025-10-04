namespace Basket.Infrastructure.Subscription.PrimaryBasketCleanup;

public class PrimaryBasketCleanupConsumer(BasketDbContext basketDbContext) : IConsumer<PrimaryBasketCleanupEvent>
{
    private readonly BasketDbContext _basketDbContext = basketDbContext;

    public async Task Consume(ConsumeContext<PrimaryBasketCleanupEvent> context)
    {

        var basket = await _basketDbContext.PrimaryUserBaskets
                                           .Include(f => f.Items)
                                           .FirstOrDefaultAsync(f => f.UserId == context.Message.UserId);

        if (basket is null)
            // error log
            return;

        basket.CleanUp();
        await _basketDbContext.SaveChangesAsync();
    }
}