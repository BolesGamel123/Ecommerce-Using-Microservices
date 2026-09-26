using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Net;
using Basket.Application.Responses;
using Basket.Application.Queries;
using Basket.Application.Commands;

namespace Basket.API.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasketByUserName(string userName)
        {
            var query = new GetBasketQuery(userName);
            var ShoppingCart = await _mediator.Send(query);
            return Ok(ShoppingCart);
        }

        [HttpPut]
        [Route("[action]", Name = "UpdateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateProduct([FromBody] UpdateBasketCommand command)
        {
            var updatedBasket = await _mediator.Send(command);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasket")]
        public async Task<ActionResult> DeleteProduct(string userName)
        {
            var command = new DeleteBasketCommand(userName);
            return Ok(await _mediator.Send(command));
        }
    }
}
