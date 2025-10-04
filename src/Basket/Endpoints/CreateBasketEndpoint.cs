namespace Basket.Endpoints;

public static class CreateBasketEndpoint
{
    public static void MapCreateBasketEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/primary/{user-id}/add-item",
             async ([FromRoute(Name = "user-id")] int UserId,
             CreateBasketItemRequest request,
             BasketDbContext dbContext) =>
             {

                 var primaryBasket = await dbContext.PrimaryUserBaskets
                                                    .FirstOrDefaultAsync(d => d.UserId == UserId);

                 if (primaryBasket is null)
                 {
                     primaryBasket = new PrimaryUserBasket
                     {
                         UserId = UserId,
                     };

                     dbContext.UserBaskets.Add(primaryBasket);
                 }

                 primaryBasket.AddItem(request.Slug, request.Price, request.CatalogItemName);
                 await dbContext.SaveChangesAsync();

                 return Results.Ok();
             });
    }
}

public record CreateBasketItemRequest(string Slug, string CatalogItemName, decimal Price);