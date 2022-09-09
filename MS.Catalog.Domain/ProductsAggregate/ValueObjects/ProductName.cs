using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Catalog.Domain.ProductsAggregate.ValueObjects
{
    public class ProductName:ValueObject
    {
        public ProductName(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator ProductName(string name)=> new ProductName(name);
        public static implicit operator string(ProductName productName) => productName.Value;

    }
}
