using Core.Domain;

namespace MS.Catalog.Domain.ProductsAggregate.ValueObjects
{
    public class ProductDescription : ValueObject
    {
        public ProductDescription(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator ProductDescription(string description) => new ProductDescription(description);
        public static implicit operator string(ProductDescription productDescription) => productDescription.Value;

    }
}
