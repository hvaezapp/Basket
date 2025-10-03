using Basket.Infrastructure.Persistence.Context;
using Basket.Shared;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

    public static void ConfigureBroker(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(configure =>
        {
            var brokerConfig = builder.Configuration.GetSection(BrokerOptions.SectionName)
                                                    .Get<BrokerOptions>();
            if (brokerConfig is null)
                throw new ArgumentNullException(nameof(BrokerOptions));

            configure.AddConsumers(Assembly.GetExecutingAssembly());

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(brokerConfig.Host, hostConfigure =>
                {
                    hostConfigure.Username(brokerConfig.Username);
                    hostConfigure.Password(brokerConfig.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

    }
}

