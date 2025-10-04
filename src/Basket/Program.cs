var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDatabase();
builder.ConfigureBroker();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapCreateBasketEndpoint();
app.MapMoveToNextEndpoint();
app.MapMoveToPrimaryEndpoint();
app.MapIncreaseQuantityEndpoint();
app.MapDecreaseQuantityEndpoint();
app.MapRemoveItemEndpoint();
app.MapBasketItemsEndpoint();

// simulate price changed event
app.MapGet("/price-changed-event", (IPublishEndpoint publisher) =>
{
    publisher.Publish(new PriceChangedEvent("test-slug", 10_000));
});


app.Run();
