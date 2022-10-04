using MediatRNotificationExample.Domain;

namespace MediatRNotificationExample.Interfaces;

public interface IProductRepository
{
    public Task<Product?> Get(string sku);
    public Task Add(Product product);
    public Task Update(Product product);
    public Task Remove(string sku);
}