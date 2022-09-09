using Core.Domain;

namespace MS.Catalog.Domain.ProductsAggregate.ValueObjects
{
    public class ProductId : ValueObject
    {
        public ProductId(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator ProductId(int id) => new ProductId(id);
        public static implicit operator int(ProductId productId) => productId.Value;

    }
}
