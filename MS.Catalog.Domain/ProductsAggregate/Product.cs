using Core.Domain;
using MS.Catalog.Domain.ProductsAggregate.ValueObjects;

namespace MS.Catalog.Domain.ProductsAggregate
{
    public class Product:BaseAggregateRoot<Product,ProductId>
    {
        private Product()
        {

        }

        public ProductName Name { get;private set; }
        public ProductDescription Description { get;private set; }
        public ProductAvailableStock AvailableStock { get; private set; }
        public ProductPrice Price { get; private set; }
        public Product(ProductId productId,
            ProductName name,
            ProductDescription description, 
            ProductAvailableStock availableStock):base(productId)
        {
            Name = name;
            Description = description;
            AvailableStock = availableStock;
        } 
        
        public void ChangePrice(ProductPrice price)
        {
            Price = price;
        }
    }
}
