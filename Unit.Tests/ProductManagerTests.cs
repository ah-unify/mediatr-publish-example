using MediatRNotificationExample;
using MediatRNotificationExample.Domain;
using MediatRNotificationExample.Interfaces;
using NSubstitute;

namespace Unit.Tests;

public class ProductManagerTests
{
    private IProductRepository _productRepository;
    private IEmailer _emailer;
    private IAuditor _auditor;
    private IEmailAddressResolver _emailAddressResolver;
    private IExternalProductSyncService _externalProductSyncService;
    
    private ProductManager _productManager;

    [SetUp]
    public void Setup()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _emailer = Substitute.For<IEmailer>();
        _auditor = Substitute.For<IAuditor>();
        _emailAddressResolver = Substitute.For<IEmailAddressResolver>();
        _externalProductSyncService = Substitute.For<IExternalProductSyncService>();
        
        _productManager = new ProductManager(_productRepository,
            _emailer, 
            _auditor, 
            _emailAddressResolver, 
            _externalProductSyncService );
    }

    [Test]
    public void Should_AddProduct_And_Audit_And_Email_And_StartSync()
    {
        // Given
        const string sku = "12345";
        const string title = "OverpricedGpu";
        const string email = "jerry.seinfeld@gmail.com";
        _emailAddressResolver.GetProductManagementEmailAddress().Returns(email);
        
        // When
        _productManager.AddProduct(sku, title);
        
        // Then
        _productRepository.Received().Add(Arg.Is<Product>(x=> x.Sku == sku && x.Title == title));
        
        _auditor.Received().Audit(sku, "product_added_audit");
        _emailer.Received().Email(email, "product_added_template");
        _externalProductSyncService.Received().SyncProductBySku(sku);
    }
    
    [Test]
    public void Should_UpdateProduct_And_Audit()
    {
        // Given an existing product
        const string sku = "12345";
        const string title = "OverpricedGpu";
        _productRepository.Get(sku).Returns(new Product(sku, title));
        
        const string newTitle = "RegularlyPricedGpu";
        
        // When
        _productManager.UpdateProduct(sku, newTitle);
        
        // Then
        _productRepository.Received().Update(Arg.Is<Product>(x=> x.Sku == sku && x.Title == newTitle));
        
        _auditor.Received().Audit(sku, "product_updated_audit");
    }
    
    [Test]
    public void Should_RemoveProduct_And_Audit_And_Email()
    {
        // Given
        const string sku = "12345";
        const string email = "jerry.seinfeld@gmail.com";
        _emailAddressResolver.GetProductManagementEmailAddress().Returns(email);
        
        // When
        _productManager.RemoveProduct(sku);
        
        // Then
        _productRepository.Received().Remove(sku);
        
        _auditor.Received().Audit(sku, "product_removed_audit");
        _emailer.Received().Email(email, "product_removed_template");
    }
}