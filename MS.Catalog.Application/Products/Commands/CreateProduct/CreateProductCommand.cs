using Core.Common.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MS.Catalog.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand(CreateProductRequest Request)
        :ICommand<CreateProductCommandResult>
    {

    }
}
