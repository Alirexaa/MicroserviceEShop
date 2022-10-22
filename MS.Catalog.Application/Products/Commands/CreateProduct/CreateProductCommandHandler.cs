using Core.Common.Messaging;
using MS.Catalog.Domain.ProductsAggregate;
using MS.Catalog.Domain.ProductsAggregate.Events;

namespace MS.Catalog.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _repository;
        private readonly IEventListener _eventListener;
        public CreateProductCommandHandler(IGuidGenerator guidGenerator, IUnitOfWork unitOfWork, IRepository<Product> repository, IEventListener eventListener)
        {
            _guidGenerator = guidGenerator;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _eventListener = eventListener;
        }

        public async Task<CreateProductCommandResult> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
        {
            var product = new Product(_guidGenerator.Create(), command.Request.Name, command.Request.Description);
            await _repository.AddAsync(product, cancellationToken);
            await _unitOfWork.Complate();
            await _eventListener.Publish(new ProductCreatedEvent(product.Id, product.Name));
            //_eventListener.Subscribe<ProductCreatedEvent>();
            return new CreateProductCommandResult(product.Id);
        }
    }
}
