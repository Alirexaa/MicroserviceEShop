using Core.Domain;

namespace MS.Catalog.Domain.ProductsAggregate.ValueObjects
{
    public class ProductId : ValueObject
    {
        public ProductId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator ProductId(Guid id) => new ProductId(id);
        public static implicit operator Guid(ProductId productId) => productId.Value;

    }
}
