using Basket.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basket.Endpoints;

public static class BasketItemsEndpoint
{
    public static void MapBasketItemsEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/primary/{user-id}",
             async ([FromRoute(Name = "user-id")] int UserId,
             BasketDbContext dbContext) =>
             {

                 var primaryBasket = await dbContext.PrimaryUserBaskets
                                                    .Include(d => d.Items)
                                                    .FirstOrDefaultAsync(d => d.UserId == UserId);

                 if (primaryBasket is null)
                 {
                     return Results.BadRequest("invalid your data!");
                 }

                 return Results.Ok(primaryBasket.Items.Select(d => new
                 {
                     d.Price,
                     d.CatalogItemName,
                     d.Slug,
                     d.Id,
                     d.Quantity,
                     d.PriceChanged,
                 }));
             });
    }
}
