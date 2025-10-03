using Basket.Bootstraper;
using Basket.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDatabase();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapCreateBasketEndpoint();
app.MapMoveToNextEndpoint();
app.MapMoveToPrimaryEndpoint();
app.MapIncreaseQuantityEndpoint();
app.MapDecreaseQuantityEndpoint();
app.MapRemoveItemEndpoint();
app.MapBasketItemsEndpoint();

app.Run();
