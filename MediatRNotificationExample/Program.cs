using System.Reflection;
using MediatR;
using MediatRNotificationExample;
using MediatRNotificationExample.Infrastructure;
using MediatRNotificationExample.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            .AddSingleton<IProductRepository, InMemoryProductRepository>()
            .AddTransient<ProductManager, ProductManager>()
            .AddTransient<IAuditor, Auditor>()
            .AddTransient<IEmailer, Emailer>()
            .AddTransient<IEmailAddressResolver, EmailAddressResolver>()
            .AddTransient<IExternalProductSyncService, ExternalProductSyncService>()
            // .AddMediatR(typeof(Program).GetTypeInfo().Assembly)// Registers MediatR
        ) 
    .Build();

using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;

var manager = provider.GetRequiredService<ProductManager>();

const string sku = "12345";

Console.WriteLine("*** Adding Product ***");
await manager.AddProduct(sku, "TestProduct");
Console.ReadLine();

Console.WriteLine("*** Updating Product ***");
await manager.UpdateProduct(sku, "NewTestProduct");
Console.ReadLine();

Console.WriteLine("*** Removing Product ***");
await manager.RemoveProduct(sku);
Console.ReadLine();