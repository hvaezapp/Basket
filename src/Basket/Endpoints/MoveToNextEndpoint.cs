namespace Basket.Endpoints;

public static class MoveToNextEndpoint
{
    public static void MapMoveToNextEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/primary/{user-id}/move/{item-id}",
             async ([FromRoute(Name = "user-id")] int UserId,
             [FromRoute(Name = "item-id")] Guid ItemId,
             BasketDbContext dbContext) =>
             {

                 var primaryBasket = await dbContext.PrimaryUserBaskets
                                                    .FirstOrDefaultAsync(d => d.UserId == UserId);

                 if (primaryBasket is null)
                 {
                     return Results.BadRequest("invalid your data!");
                 }


                 var item = primaryBasket.Items.FirstOrDefault(d => d.Id == ItemId);
                 if (item is null)
                 {
                     return Results.BadRequest("invalid your data!");
                 }

                 var secondrayBasket = await dbContext.SecondaryUserBaskets
                                                    .FirstOrDefaultAsync(d => d.UserId == UserId);

                 if (secondrayBasket is null)
                 {
                     secondrayBasket = new SecondaryUserBasket
                     {
                         UserId = UserId,
                     };
                     dbContext.UserBaskets.Add(secondrayBasket);
                 }


                 secondrayBasket.AddItem(item);
                 primaryBasket.Items.Remove(item);
                 await dbContext.SaveChangesAsync();

                 return Results.Ok();
             });
    }
}
