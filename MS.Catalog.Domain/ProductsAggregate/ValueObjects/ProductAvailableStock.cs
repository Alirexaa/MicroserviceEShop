using Core.Domain;

namespace MS.Catalog.Domain.ProductsAggregate.ValueObjects
{
    public class ProductAvailableStock : ValueObject
    {
        public ProductAvailableStock(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator ProductAvailableStock(int availableStock) => new ProductAvailableStock(availableStock);
        public static implicit operator int(ProductAvailableStock productAvailableStock) => productAvailableStock.Value;

    }
}
