namespace Basket.Endpoints;

public static class MoveToPrimaryEndpoint
{
    public static void MapMoveToPrimaryEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/secondary/{user-id}/move/{item-id}",
             async ([FromRoute(Name = "user-id")] int UserId,
             [FromRoute(Name = "item-id")] Guid ItemId,
             BasketDbContext dbContext) =>
             {
                 var secondrayBasket = await dbContext.SecondaryUserBaskets
                                             .FirstOrDefaultAsync(d => d.UserId == UserId);

                 if (secondrayBasket is null)
                 {
                     return Results.BadRequest("invalid your data!");
                 }


                 var item = secondrayBasket.Items.FirstOrDefault(d => d.Id == ItemId);
                 if (item is null)
                 {
                     return Results.BadRequest("invalid your data!");
                 }


                 var primaryBasket = await dbContext.PrimaryUserBaskets
                                       .FirstAsync(d => d.UserId == UserId);

                 primaryBasket.AddItem(item);
                 secondrayBasket.Items.Remove(item);
                 await dbContext.SaveChangesAsync();

                 return Results.Ok();
             });
    }
}
