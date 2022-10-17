using MS.Catalog.Domain.ProductsAggregate;


namespace MS.Catalog.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _repository;
        public CreateProductCommandHandler(IGuidGenerator guidGenerator, IUnitOfWork unitOfWork, IRepository<Product> repository)
        {
            _guidGenerator = guidGenerator;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<CreateProductCommandResult> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
        {
            var product = new Product(_guidGenerator.Create(), command.Request.Name, command.Request.Description);
            await _repository.AddAsync(product, cancellationToken);
            await _unitOfWork.Complate();
            return new CreateProductCommandResult(product.Id);
        }
    }
}
