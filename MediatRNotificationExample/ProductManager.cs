using MediatRNotificationExample.Domain;
using MediatRNotificationExample.Interfaces;

namespace MediatRNotificationExample;

public class ProductManager
{
    private readonly IProductRepository _productRepository;
    private readonly IEmailer _emailer;
    private readonly IAuditor _auditor;
    private readonly IEmailAddressResolver _emailAddressResolver;
    private readonly IExternalProductSyncService _externalProductSyncService;

    public ProductManager(IProductRepository productRepository, 
        IEmailer emailer,
        IAuditor auditor,
        IEmailAddressResolver emailAddressResolver,
        IExternalProductSyncService externalProductSyncService)
    {
        _productRepository = productRepository;
        _emailer = emailer;
        _auditor = auditor;
        _emailAddressResolver = emailAddressResolver;
        _externalProductSyncService = externalProductSyncService;
    }
    
    /// <summary>
    /// Adds a product, emails product management, audits the process and begins an external sync
    /// </summary>
    public async Task AddProduct(string sku, string title)
    {
        var product = new Product(sku, title);
        await _productRepository.Add(product);

        var email = await _emailAddressResolver.GetProductManagementEmailAddress();
        await _emailer.SendTemplatedEmail(email, "product_added_template");

        await _auditor.Audit(sku, "product_added_audit");

        await _externalProductSyncService.SyncProductBySku(sku);
    }
    
    /// <summary>
    /// Updates a product and audits
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

        await _auditor.Audit(sku, "product_updated_audit");
    }
    
    /// <summary>
    /// Removes a product, emails product management, audits the process
    /// </summary>
    public async Task RemoveProduct(string sku)
    {
        await _productRepository.Remove(sku);
        
        var email = await _emailAddressResolver.GetProductManagementEmailAddress();
        await _emailer.SendTemplatedEmail(email, "product_removed_template");

        await _auditor.Audit(sku, "product_removed_audit");
    }
}