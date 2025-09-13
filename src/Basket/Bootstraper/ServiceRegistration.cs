using Basket.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Basket.Bootstraper;

public static class ServiceRegistration
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<BasketDbContext>(configure =>
        {
            configure.UseSqlServer(builder.Configuration.GetConnectionString(BasketDbContext.DefaultConnectionStringName));
        });
    }
}
