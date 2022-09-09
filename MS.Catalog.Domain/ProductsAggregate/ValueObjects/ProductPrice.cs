using Core.Domain;

namespace MS.Catalog.Domain.ProductsAggregate.ValueObjects
{
    public class ProductPrice : ValueObject
    {
        public ProductPrice(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator ProductPrice(decimal price) => new ProductPrice(price);
        public static implicit operator decimal(ProductPrice productPrice) => productPrice.Value;

    }
}
