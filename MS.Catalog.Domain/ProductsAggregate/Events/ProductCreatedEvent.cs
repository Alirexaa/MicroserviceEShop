using Core.Domain;
using MS.Catalog.Domain.ProductsAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Catalog.Domain.ProductsAggregate.Events
{
    public class ProductCreatedEvent : BaseDomainEvent<Product,ProductId>

    {
        public ProductCreatedEvent(Guid productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }

        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
    }
}
