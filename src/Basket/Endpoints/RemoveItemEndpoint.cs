using Basket.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basket.Endpoints;

public static class RemoveItemEndpoint
{
    public static void MapRemoveItemEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete("/primary/{item-id}/remove",
             async ([FromRoute(Name = "item-id")] Guid ItemId,
             BasketDbContext dbContext) =>
             {

                 var primaryBasket = await dbContext.PrimaryUserBaskets
                                                    .Include(d => d.Items)
                                                    .FirstOrDefaultAsync(d => d.Items.Any(f => f.Id == ItemId));

                 if (primaryBasket is null)
                 {
                     return Results.BadRequest("invalid your data!");
                 }

                 primaryBasket.Remove(ItemId);
                 await dbContext.SaveChangesAsync();

                 return Results.Ok();
             });
    }
}
