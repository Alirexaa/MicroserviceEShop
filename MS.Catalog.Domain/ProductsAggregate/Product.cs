using Core.Domain;
using MS.Catalog.Domain.ProductsAggregate.Events;
using MS.Catalog.Domain.ProductsAggregate.ValueObjects;

namespace MS.Catalog.Domain.ProductsAggregate
{
    public class Product : BaseAggregateRoot<Product, ProductId>
    {
        #region private_ctor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Product()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        #endregion


        public ProductName Name { get; private set; }
        public ProductDescription Description { get; private set; }
        public ProductAvailableStock AvailableStock { get; private set; } = default!;
        public ProductPrice Price { get; private set; } = default!;
        public Product(ProductId productId,
            ProductName name,
            ProductDescription description) : base(productId)
        {
            Name = name;
            Description = description;
            AvailableStock = 0;
            Price = 0;

            AddDomainEvent(new ProductCreatedEvent(Id, name));
        }

        public void ChangePrice(ProductPrice price)
        {
            Price = price;
        }
    }
}
