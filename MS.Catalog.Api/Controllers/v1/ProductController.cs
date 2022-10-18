using Core.Common.Cqrs.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Catalog.Application.Products.Commands.CreateProduct;
using System.Net;

namespace MS.Catalog.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    //[Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public ProductController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }


        [Route("items")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProductAsync(CreateProductRequest createProductRequest)
        {
            var result = await _commandDispatcher
                 .SendAsync<CreateProductCommand, CreateProductCommandResult>(new CreateProductCommand(createProductRequest));

            return CreatedAtRoute(nameof(ProductByIdAsync), new { id = result.ProductId });
        }


        [Route("items/{id:int}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ProductByIdAsync(Guid id)
        {
            return Ok();
        }
    }
}
