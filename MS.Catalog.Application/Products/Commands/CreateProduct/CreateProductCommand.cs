namespace MS.Catalog.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand(CreateProductRequest Request)
        :ICommand<CreateProductCommandResult>
    {

    }
}
