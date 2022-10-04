using MediatRNotificationExample.Domain;
using MediatRNotificationExample.Interfaces;

namespace MediatRNotificationExample.Infrastructure;

public class InMemoryProductRepository : IProductRepository
{
    private List<Product> _products = new();

    public Task<Product?> Get(string sku)
    {
        var product = _products.FirstOrDefault(x => x.Sku == sku);
        return Task.FromResult(product);
    }

    public Task Add(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task Update(Product product)
    {
        var productInMemory = _products.FirstOrDefault(x => x.Sku == product.Sku);
        productInMemory = product;
        return Task.CompletedTask;
    }

    public Task Remove(string sku)
    {
        _products.RemoveAll(x => x.Sku == sku);
        return Task.CompletedTask;
    }
}