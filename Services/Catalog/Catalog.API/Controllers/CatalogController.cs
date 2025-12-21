using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{

    public class CatalogController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetAllProductsByName")]
        [ProducesResponseType(typeof(IList<ProductResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponseDto>>> GetAllProductsByName(string productName)
        {
            var query = new GetProductsByNameQuery(productName);
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponseDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductsQuery(catalogSpecParams);
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpPost]
        [Route("[action]", Name = "CreateProduct")]
        [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var createdProduct = await _mediator.Send(command);
            return Ok(createdProduct);

        }

        [HttpPut]
        [Route("[action]", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var updatedProduct = await _mediator.Send(command);
            return Ok(updatedProduct);

        }


        [HttpDelete]
        [Route("[action]/{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return Ok(result);
        }


        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypeResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<TypeResponseDto>>> GetAllTypes()
        {
            var query = new GetAllTypeQuery();
            var types = await _mediator.Send(query);
            return Ok(types);
        }


        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<BrandResponseDto>>> GetAllBrands()
        {
            var query = new GetAllBrandQuery();
            var brands = await _mediator.Send(query);
            return Ok(brands);
        }
    }
}
