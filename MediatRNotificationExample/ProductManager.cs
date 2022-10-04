using MediatR;
using MediatRNotificationExample.Domain;
using MediatRNotificationExample.Interfaces;
using MediatRNotificationExample.Notifications;

namespace MediatRNotificationExample;

public class ProductManager
{
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;

    public ProductManager(IProductRepository productRepository, 
        IMediator mediator)
    {
        _productRepository = productRepository;
        _mediator = mediator;
    }
    
    /// <summary>
    /// Adds a product, raises a notification
    /// </summary>
    public async Task AddProduct(string sku, string title)
    {
        var product = new Product(sku, title);
        await _productRepository.Add(product);

        await _mediator.Publish(new ProductAddedNotification(sku));
    }
    
    /// <summary>
    /// Updates a product, raises a notification
    /// </summary>
    public async Task UpdateProduct(string sku, string newTitle)
    {
        var product = await _productRepository.Get(sku);
        if (product == null)
        {
            throw new InvalidDataException("Product not found");
        }
        
        product.UpdateTitle(newTitle);
        await _productRepository.Update(product);

        await _mediator.Publish(new ProductUpdatedNotification(sku));
    }
    
    /// <summary>
    /// Removes a product, raises a notification
    /// </summary>
    public async Task RemoveProduct(string sku)
    {
        await _productRepository.Remove(sku);
        
        await _mediator.Publish(new ProductRemovedNotification(sku));
    }
}