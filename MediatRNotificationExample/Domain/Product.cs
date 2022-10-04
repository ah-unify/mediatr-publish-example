namespace MediatRNotificationExample.Domain;

public class Product
{
    public string Sku { get; }
    public string Title { get; protected set; }

    public Product(string sku, string title)
    {
        Sku = sku;
        Title = title;
    }

    public void UpdateTitle(string title)
    {
        Title = title;
    }
}