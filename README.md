#  Basket Service

This project is a **Basket Service** for an e-commerce platform, built with **ASP.NET Core 9 Minimal API**.  
It provides efficient and scalable management of users’ shopping baskets, handling item addition, removal, and synchronization with catalog changes.

---

## Technologies Used

- **C#**
- **.NET Core 9**
- **ASP.NET Core Minimal API**
- **RabbitMQ** — used for handling domain events such as:
  - `PrimaryBasketCleanup`
  - `PriceChanged`
  - `CatalogNameChanged`
  - `CatalogItemRemoved`

---

## 🔗 API Endpoints

| Endpoint | Description |
|-----------|-------------|
| `BasketItemsEndpoint` | Retrieves items in a user’s basket |
| `CreateBasketEndpoint` | Creates a new basket for a user |
| `IncreaseQuantityEndpoint` | Increases the quantity of a specific item |
| `DecreaseQuantityEndpoint` | Decreases the quantity of a specific item |
| `RemoveItemEndpoint` | Removes an item from the basket |
| `MoveToNextEndpoint` | Moves items from the primary basket to the next basket |
| `MoveToPrimaryEndpoint` | Moves items back to the primary basket |

